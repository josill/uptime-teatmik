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
        Entity entity;
        
        try
        {
            var parsedEntity = BusinessRegisterParser.ParseEntity(responseContent);
            
            var existingEntity =
                await dbContext.Entities
                    .Include(e => e.OwnedEntities)
                    .FirstOrDefaultAsync(b => b.BusinessOrPersonalCode == businessCode);
            
            // TODO: check for changes / updates
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
    
    private async Task UpdateBusinessRelatedPersons(string responseContent, Entity entity)
    {
        var relatedEntitiesJson = BusinessRegisterParser.ParseBusinessRelatedEntities(responseContent);

        foreach (var relatedEntity in relatedEntitiesJson)
        {
            var parsedRelatedEntity = new ParsedRelatedEntity(relatedEntity);
            var owner = await dbContext.Entities.FirstOrDefaultAsync(e =>
                e.BusinessOrPersonalCode == parsedRelatedEntity.PersonalOrBusinessCode) ?? new Entity()
            {
                Id = Guid.NewGuid(),
                FirstName = parsedRelatedEntity.FirstName,
                BusinessOrLastName = parsedRelatedEntity.LastOrBusinessName,
                BusinessOrPersonalCode = parsedRelatedEntity.PersonalOrBusinessCode,
                EntityTypeAbbreviation = parsedRelatedEntity.EntityTypeAbbreviation,
                EntityType = parsedRelatedEntity.EntityType
            };

            if (owner.OwnedEntities.Contains(owner)) continue;
            var ownerRelation = new EntityOwner()
            {
                Id = Guid.NewGuid(),
                Owned = entity,
                Owner = owner,
                RoleInEntity = parsedRelatedEntity.EntityType,
                RoleInEntityAbbreviation = parsedRelatedEntity.EntityTypeAbbreviation
            };
            dbContext.EntityOwners.Add(ownerRelation);
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