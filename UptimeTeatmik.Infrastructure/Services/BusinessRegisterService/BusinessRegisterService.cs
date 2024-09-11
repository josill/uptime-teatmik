using System.Text;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;
using UptimeTeatmik.Domain;
using UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
                    FormattedJson = parsedEntity.FormattedJson,
                    UniqueCode = parsedEntity.UniqueCode
                };

                entity = newEntity;
                dbContext.Entities.Add(newEntity);
            }

            await UpdateBusinessRelatedPersons(responseContent, entity);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            // TODO: Correctly handle the error
        }
    }
    
    private async Task UpdateBusinessRelatedPersons(string responseContent, Entity owned)
    {
        var relatedEntitiesJson = BusinessRegisterParser.ParseBusinessRelatedEntities(responseContent);
        var parsedRelatedEntities = relatedEntitiesJson.Select(re => new ParsedRelatedEntity(re)).ToList();

        foreach (var pe in parsedRelatedEntities)
        {
            var existingRelation = await GetExistingRelation(owned.Id, pe.BusinessOrLastName); 
            if (existingRelation != null) continue; // Here we would check for changes in relation to the owned business

            var owner = await GetExistingOwner(pe.BusinessOrLastName) ?? MapParsedRelatedEntityToEntity(pe);
            dbContext.Entities.Add(owner);

            var newRelation = new EntityOwner()
            {
                Id = Guid.NewGuid(),
                Owned = owned,
                Owner = owner,
                RoleInEntity = pe.EntityType,
                RoleInEntityAbbreviation = pe.EntityTypeAbbreviation
            };
            dbContext.EntityOwners.Add(newRelation);
        }
    }
    
    private async Task<string> GetXmlResponseContentAsync(string body, string endPointUrl)
    {
        var content = new StringContent(body, Encoding.UTF8, "text/xml");
        var response = await httpClient.PostAsync(endPointUrl, content);
        
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }

    private async Task<EntityOwner?> GetExistingRelation(Guid ownedId, string ownerBusinessOrLastName)
    {
        var existingRelation = await dbContext.EntityOwners
            .Include(eo => eo.Owner)
            .Where(eo => eo.OwnedId == ownedId && eo.Owner.BusinessOrLastName == ownerBusinessOrLastName)
            .FirstOrDefaultAsync();

        return existingRelation;
    }

    private async Task<Entity?> GetExistingOwner(string businessOrLastName)
    {
        var existingOwner = await dbContext.Entities
            .FirstOrDefaultAsync(e =>
                e.BusinessOrLastName != null &&
                e.BusinessOrLastName.ToLower() == businessOrLastName.ToLower());

        return existingOwner;
    }

    private static Entity MapParsedRelatedEntityToEntity(ParsedRelatedEntity parsedEntity)
    {
        var newOwner = new Entity()
        {
            Id = new Guid(),
            FirstName = parsedEntity.FirstName,
            BusinessOrLastName = parsedEntity.BusinessOrLastName,
            BusinessOrPersonalCode = parsedEntity.BusinessOrPersonalCode,
            EntityType = parsedEntity.EntityType,
            EntityTypeAbbreviation = parsedEntity.EntityTypeAbbreviation,
            UniqueCode = parsedEntity.UniqueCode
        };

        return newOwner;
    }
}