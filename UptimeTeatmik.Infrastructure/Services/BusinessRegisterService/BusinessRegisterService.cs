using System.Text;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;
using UptimeTeatmik.Domain;
using UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;

public class BusinessRegisterService(IAppDbContext dbContext, HttpClient httpClient, IOptions<BusinessRegisterSettings> settings, IBusinessRegisterBodyGenerator businessRegisterBodyGenerator) : IBusinessRegisterService
{
    public async Task RunBusinessUpdateJob()
    {
        var dateNow = DateTime.Now;
        await FetchUpdatedBusinessCodesAsync(dateNow);
    }
    
    public async Task<List<string>> FetchUpdatedBusinessCodesAsync(DateTime date)
    {
        var body = businessRegisterBodyGenerator.GenerateChangesUrlXmlBody(date);
        var responseContent = await GetXmlResponseContentAsync(body, settings.Value.ChangesUrl);
        
        var doc = XDocument.Parse(responseContent);
        var ns = "{http://arireg.x-road.eu/producer/}";
        List<string> businessCodes = [];

        foreach (var element in doc.Descendants(ns + "ettevotja_muudatused"))
        {
            var businessCode = element.Element(ns + "ariregistri_kood")?.Value;

            if (businessCode == null) continue;
            businessCodes.Add(businessCode);
        }
        
        return businessCodes;
    }

    public async Task UpdateBusinessesAsync(List<string> businessesCodes)
    {
        foreach (var businessCode in businessesCodes)
        {
            try
            {
                await UpdateBusinessAsync(businessCode);
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update business with code {businessCode} with exception: {ex}");
            }
        }
    }

    private async Task UpdateBusinessAsync(string businessCode)
    {
        var body = businessRegisterBodyGenerator.GenerateDetailDataUrlXmlBody(businessCode);
        var responseContent = await GetXmlResponseContentAsync(body, settings.Value.DetailDataUrl);

        try
        {
            var parsedEntity = BusinessRegisterParser.ParseEntity(responseContent);
            
            var existingEntity =
                await dbContext.Entities
                    .FirstOrDefaultAsync(b => b.BusinessOrPersonalCode == businessCode);
            
            // TODO: check for changes / updates
            Entity entity;
            if (existingEntity != null)
            {
                existingEntity.BusinessOrLastName = parsedEntity.LastOrBusinessName;
                existingEntity.FormattedJson = parsedEntity.FormattedJson;
                existingEntity.EntityType = parsedEntity.EntityType;
                existingEntity.EntityTypeAbbreviation = parsedEntity.EntityTypeAbbreviation;

                entity = existingEntity;
                dbContext.Entities.Update(existingEntity);
            }
            else
            {
                var newEntity = new Entity()
                {
                    Id = Guid.NewGuid(),
                    BusinessOrPersonalCode = businessCode,
                    BusinessOrLastName = parsedEntity.LastOrBusinessName,
                    EntityType = parsedEntity.EntityType,
                    EntityTypeAbbreviation = parsedEntity.EntityTypeAbbreviation,
                    FormattedJson = parsedEntity.FormattedJson
                };

                entity = newEntity;
                dbContext.Entities.Add(newEntity);
            }

            await UpdateBusinessRelatedPersons(responseContent, entity);
            await dbContext.SaveChangesAsync();
        }
        catch (JsonException)
        {
            // TODO: Correctly handle the error
        }
    }
    
    private async Task UpdateBusinessRelatedPersons(string responseContent, Entity ownedEntity)
    {
        var relatedEntitiesJson = BusinessRegisterParser.ParseBusinessRelatedEntities(responseContent);
        var parsedRelatedEntities = relatedEntitiesJson.Select(re => new ParsedRelatedEntity(re)).ToList();
        
        var entityUniqueCodes = parsedRelatedEntities.Select(pre => pre.UniqueCode).ToList();

        var existingOwners = await dbContext.Entities
            .Where(e => entityUniqueCodes.Contains(e.UniqueCode))
            .ToDictionaryAsync(e => e.BusinessOrPersonalCode, e => e);

        var existingRelations = await dbContext.EntityOwners
            .Include(eo => eo.Owner)
            .Where(eo => eo.Owned.Id == ownedEntity.Id)
            .ToDictionaryAsync(eo => eo.Owner.BusinessOrPersonalCode, eo => eo);

        List<Entity> newOwners = [];
        List<EntityOwner> newRelations = [];
        List<EntityOwner> updatedRelations = [];

        foreach (var parsedRelatedEntity in parsedRelatedEntities)
        {
            if (!existingOwners.TryGetValue(parsedRelatedEntity.BusinessOrPersonalCode, out var owner))
            {
                owner = new Entity
                {
                    Id = Guid.NewGuid(),
                    FirstName = parsedRelatedEntity.FirstName,
                    BusinessOrLastName = parsedRelatedEntity.BusinessOrLastName,
                    BusinessOrPersonalCode = parsedRelatedEntity.BusinessOrPersonalCode,
                    EntityTypeAbbreviation = parsedRelatedEntity.EntityTypeAbbreviation,
                    EntityType = parsedRelatedEntity.EntityType
                };
                newOwners.Add(owner);
                existingOwners[parsedRelatedEntity.BusinessOrPersonalCode] = owner;
            }

            if (!existingRelations.TryGetValue(parsedRelatedEntity.BusinessOrPersonalCode, out var relation))
            {
                relation = new EntityOwner
                {
                    Id = Guid.NewGuid(),
                    Owned = ownedEntity,
                    Owner = owner,
                    RoleInEntity = parsedRelatedEntity.EntityType,
                    RoleInEntityAbbreviation = parsedRelatedEntity.EntityTypeAbbreviation
                };
                newRelations.Add(relation);
            }
            else if (relation.RoleInEntity != parsedRelatedEntity.EntityType || 
                     relation.RoleInEntityAbbreviation != parsedRelatedEntity.EntityTypeAbbreviation)
            {
                relation.RoleInEntity = parsedRelatedEntity.EntityType;
                relation.RoleInEntityAbbreviation = parsedRelatedEntity.EntityTypeAbbreviation;
                updatedRelations.Add(relation);
            }
        }

        if (newOwners.Any())
        {
            await dbContext.Entities.AddRangeAsync(newOwners);
        }

        if (newRelations.Any())
        {
            await dbContext.EntityOwners.AddRangeAsync(newRelations);
        }

        if (updatedRelations.Any())
        {
            dbContext.EntityOwners.UpdateRange(updatedRelations);
        }

        await dbContext.SaveChangesAsync();
    }
    
    private async Task<string> GetXmlResponseContentAsync(string body, string endPointUrl)
    {
        var content = new StringContent(body, Encoding.UTF8, "text/xml");
        var response = await httpClient.PostAsync(endPointUrl, content);
        
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }
}