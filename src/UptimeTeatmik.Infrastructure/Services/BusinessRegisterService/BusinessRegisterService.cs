using System.Text;
using System.Xml.Linq;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;
using UptimeTeatmik.Application.Common.Interfaces.NotificationService;
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
        
        var businessCodes = doc.Descendants(ns + "ettevotja_muudatused")
            .Select(element => element.Element(ns + "ariregistri_kood")?.Value)
            .Where(code => !string.IsNullOrEmpty(code))
            .Cast<string>()
            .ToList();
        
        notificationService.CreateNotificationJob(EventType.Created, $"Started fetching {businessCodes.Count} updated businesses for the date: {date:dd/MM/yyyy}");

        await UpdateBusinessesAsync(businessCodes);
        return businessCodes;
    }

    public async Task UpdateBusinessesAsync(List<string> businessCodes)
    {
        var tasks = businessCodes.Select(async businessCode =>
        {
            try
            {
                BackgroundJob.Enqueue(() => UpdateBusinessAsync(businessCode));
            }
            catch (Exception ex)
            {
                notificationService.CreateNotificationJob(EventType.UpdateFailed, ex.Message, businessCode: businessCode);
            }
        });
 
        await Task.WhenAll(tasks);
    }

    public async Task<Entity?> UpdateBusinessAsync(string businessCode)
    {
        var body = businessRegisterBodyGenerator.GenerateDetailDataUrlXmlBody(businessCode);
        var responseContent = await GetXmlResponseContentAsync(body, settings.Value.DetailDataUrl);
        Entity? entity = null;

        try
        {
            var parsedEntity = BusinessRegisterParser.ParseEntity(responseContent);
            entity = await ProcessEntityUpdate(parsedEntity, businessCode);
            await UpdateBusinessRelatedPersons(responseContent, entity);
        }
        catch (Exception ex)
        {
            notificationService.CreateNotificationJob(EventType.UpdateFailed, ex.Message, businessCode: businessCode);
        }

        return entity;
    }
    
    private async Task<Entity> ProcessEntityUpdate(ParsedEntity parsedEntity, string businessCode)
    {
        var entity = await GetExistingOwner(businessCode);
        List<string> updates = [];
        var wasCreated = false;
 
        if (entity != null)
        {
            updates = CheckAndUpdate(entity, parsedEntity);
            if (updates.Count > 0) dbContext.Entities.Update(entity);
        }
        else
        {
            entity = MapParsedEntityToEntity(parsedEntity);
            dbContext.Entities.Add(entity);
            wasCreated = true;
        }
 
        await dbContext.SaveChangesAsync();

        var wasUpdated = updates.Count > 0;
        if (wasCreated || wasUpdated)
        {
            var eventType = wasUpdated ? EventType.Updated : EventType.Created;
            var changesDetail = $"Changes: {string.Join(", ", updates)}"; 
            var comment = wasUpdated
                ? $"Business {entity.BusinessOrLastName} data changed. {changesDetail}"
                : $"Business {entity.BusinessOrLastName} created";
        
            notificationService.CreateNotificationJob(eventType, comment, entity.Id, businessCode, updates);
        }
 
        return entity;
    }
    
    private async Task UpdateBusinessRelatedPersons(string responseContent, Entity owned)
    {
        var relatedEntitiesJson = BusinessRegisterParser.ParseBusinessRelatedEntities(responseContent);
        var parsedRelatedEntities = relatedEntitiesJson.Select(re => new ParsedRelatedEntity(re)).ToList();
 
        var tasks = parsedRelatedEntities.Select(async pe =>
        {
            var existingRelation = await GetExistingRelation(owned.Id, pe.BusinessOrLastName);
            if (existingRelation == null)
            {
                var owner = await GetExistingOwner(pe.BusinessOrPersonalCode ?? string.Empty) 
                            ?? MapParsedRelatedEntityToEntity(pe);
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
        });
 
        await Task.WhenAll(tasks);
    }
    
    private async Task<string> GetXmlResponseContentAsync(string body, string endPointUrl)
    {
        var content = new StringContent(body, Encoding.UTF8, "text/xml");
        var response = await httpClient.PostAsync(endPointUrl, content);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync();
    }

    private async Task<EntityOwner?> GetExistingRelation(Guid ownedId, string ownerBusinessOrLastName)
    {
        return await dbContext.EntityOwners
            .Include(eo => eo.Owner)
            .Where(eo => eo.OwnedId == ownedId && eo.Owner.BusinessOrLastName == ownerBusinessOrLastName)
            .FirstOrDefaultAsync();
    }

    private async Task<Entity?> GetExistingOwner(string businessOrPersonalCode)
    {
        return await dbContext.Entities
            .FirstOrDefaultAsync(e => e.BusinessOrPersonalCode == businessOrPersonalCode.Trim());
    }

    private static List<string> CheckAndUpdate(Entity oldEntity, ParsedEntity newEntity)
    {
        List<string> changes = [];

        if (oldEntity.BusinessOrLastName != newEntity.BusinessOrLastName) oldEntity.BusinessOrLastName = newEntity.BusinessOrLastName;
        if (oldEntity.EntityType != newEntity.EntityType) oldEntity.EntityType = newEntity.EntityType;
        if (oldEntity.EntityTypeAbbreviation != newEntity.EntityTypeAbbreviation) oldEntity.EntityTypeAbbreviation = newEntity.EntityTypeAbbreviation;

        if (oldEntity.FormattedJson == null || newEntity.FormattedJson == null) return changes;
        var updatedParams = CheckAndUpdateFormattedJson(oldEntity.FormattedJson, newEntity.FormattedJson);
        changes.AddRange(updatedParams);

        return changes;
    }

    private static List<string> CheckAndUpdateFormattedJson(string oldJson, string newJson)
    {
        
        
        return [];
    }

    private static Entity MapParsedEntityToEntity(ParsedEntity parsedEntity)
    {
        return new Entity()
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
    }

    private static Entity MapParsedRelatedEntityToEntity(ParsedRelatedEntity parsedEntity)
    {
        return new Entity()
        {
            Id = Guid.NewGuid(),
            FirstName = parsedEntity.FirstName,
            BusinessOrLastName = parsedEntity.BusinessOrLastName,
            BusinessOrPersonalCode = parsedEntity.BusinessOrPersonalCode,
            EntityType = parsedEntity.EntityType,
            EntityTypeAbbreviation = parsedEntity.EntityTypeAbbreviation,
            UniqueCode = parsedEntity.UniqueCode
        };
    }
}