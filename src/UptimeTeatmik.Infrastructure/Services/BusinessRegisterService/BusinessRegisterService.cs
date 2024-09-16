using System.Text;
using System.Xml.Linq;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;
using UptimeTeatmik.Domain.Enums;
using UptimeTeatmik.Domain.Models;
using UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;

public class BusinessRegisterService(
    IAppDbContext dbContext, 
    HttpClient httpClient, 
    IOptions<BusinessRegisterSettings> settings, 
    IBusinessRegisterBodyGenerator businessRegisterBodyGenerator,
    INotificationService notificationService) : IBusinessRegisterService
{
    public async Task RunBusinessUpdateJob()
    {
        var dateNow = DateTime.UtcNow;
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

        BackgroundJob.Enqueue(() => notificationService.CreateNotificationAsync(EventType.Created,
            $"Started fetching {businessCodes.Count} updated businesses on {date:dd/MM/yyyy HH:mm:ss}"));

        return businessCodes;
    }

    public async Task UpdateBusinessesAsync(List<string> businessCodes)
    {
        foreach (var businessCode in businessCodes)
        {
            try
            {
                BackgroundJob.Enqueue(() => UpdateBusinessAsync(businessCode)); // Ideally we would use a Batch job here, but it is a paid feature
            }
            catch (Exception ex)
            {
                BackgroundJob.Enqueue(() =>
                    notificationService.CreateNotificationAsync(EventType.UpdateFailed, ex.Message, businessCode));
            }
        }
    }

    public async Task<Entity?> UpdateBusinessAsync(string businessCode)
    {
        var body = businessRegisterBodyGenerator.GenerateDetailDataUrlXmlBody(businessCode);
        var responseContent = await GetXmlResponseContentAsync(body, settings.Value.DetailDataUrl);
        Entity? entity = null;

        try
        {
            var parsedEntity = BusinessRegisterParser.ParseEntity(responseContent);
            var existingEntity = await GetExistingOwner(businessCode);

            var wasUpdated = false;
            var wasCreated = false;
            if (existingEntity != null)
            {
                wasUpdated = UpdateExistingEntity(existingEntity, parsedEntity);
                entity = existingEntity;
                dbContext.Entities.Update(existingEntity);
            }
            else
            {
                wasCreated = true;
                var newEntity = MapParsedEntityToEntity(parsedEntity);
                entity = newEntity;
                dbContext.Entities.Add(newEntity);
            }

            await dbContext.SaveChangesAsync();

            if (wasCreated || wasUpdated)
            {
                var eventType = wasUpdated ? EventType.Updated : EventType.Created;
                var comment = wasUpdated
                    ? $"Business {entity.BusinessOrLastName} data changed"
                    : $"Business {entity.BusinessOrLastName} created";
                BackgroundJob.Enqueue(() =>
                    notificationService.CreateNotificationAsync(eventType, comment, entity.Id, businessCode));
            }

            await UpdateBusinessRelatedPersons(responseContent, entity);
        }
        catch (Exception ex)
        {
            BackgroundJob.Enqueue(() =>
                notificationService.CreateNotificationAsync(EventType.UpdateFailed, ex.Message, businessCode));
        }

        return entity;
    }
    
    private async Task UpdateBusinessRelatedPersons(string responseContent, Entity owned)
    {
        var relatedEntitiesJson = BusinessRegisterParser.ParseBusinessRelatedEntities(responseContent);
        var parsedRelatedEntities = relatedEntitiesJson.Select(re => new ParsedRelatedEntity(re)).ToList();

        foreach (var pe in parsedRelatedEntities)
        {
            var existingRelation = await GetExistingRelation(owned.Id, pe.BusinessOrLastName); 
            if (existingRelation != null) continue; // Here we would check for changes in relation to the owned business

            var owner = await GetExistingOwner(pe.BusinessOrPersonalCode ?? string.Empty) ?? MapParsedRelatedEntityToEntity(pe);
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

    private async Task<Entity?> GetExistingOwner(string businessOrPersonalCode)
    {
        var existingOwner = await dbContext.Entities
            .FirstOrDefaultAsync(e => e.BusinessOrPersonalCode == businessOrPersonalCode.Trim());

        return existingOwner;
    }

    private static bool UpdateExistingEntity(Entity oldEntity, ParsedEntity newEntity)
    {
        var hasChanged = false;

        if (oldEntity.BusinessOrLastName != newEntity.BusinessOrLastName)
        {
            oldEntity.BusinessOrLastName = newEntity.BusinessOrLastName;
            hasChanged = true;
        }

        if (oldEntity.FormattedJson != newEntity.FormattedJson)
        {
            oldEntity.FormattedJson = newEntity.FormattedJson;
            hasChanged = true;
        }

        if (oldEntity.EntityType != newEntity.EntityType)
        {
            oldEntity.EntityType = newEntity.EntityType;
            hasChanged = true;
        }

        if (oldEntity.EntityTypeAbbreviation != newEntity.EntityTypeAbbreviation)
        {
            oldEntity.EntityTypeAbbreviation = newEntity.EntityTypeAbbreviation;
            hasChanged = true;
        }

        return hasChanged;
    }

    private static Entity MapParsedEntityToEntity(ParsedEntity parsedEntity)
    {
        var newEntity = new Entity()
        {
            Id = Guid.NewGuid(),
            // BusinessOrPersonalCode = businessCode,
            BusinessOrPersonalCode = parsedEntity.PersonalOrBusinessCode,
            BusinessOrLastName = parsedEntity.BusinessOrLastName,
            EntityType = parsedEntity.EntityType,
            EntityTypeAbbreviation = parsedEntity.EntityTypeAbbreviation,
            FormattedJson = parsedEntity.FormattedJson,
            UniqueCode = parsedEntity.UniqueCode
        };

        return newEntity;
    }

    private static Entity MapParsedRelatedEntityToEntity(ParsedRelatedEntity parsedEntity)
    {
        var newEntity = new Entity()
        {
            Id = Guid.NewGuid(),
            FirstName = parsedEntity.FirstName,
            BusinessOrLastName = parsedEntity.BusinessOrLastName,
            BusinessOrPersonalCode = parsedEntity.BusinessOrPersonalCode,
            EntityType = parsedEntity.EntityType,
            EntityTypeAbbreviation = parsedEntity.EntityTypeAbbreviation,
            UniqueCode = parsedEntity.UniqueCode
        };

        return newEntity;
    }
}