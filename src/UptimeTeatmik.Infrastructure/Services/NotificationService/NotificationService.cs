using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Domain.Enums;
using UptimeTeatmik.Domain.Models;

namespace UptimeTeatmik.Infrastructure.Services.NotificationService;

public class NotificationService(IAppDbContext dbContext) : INotificationService
{
    Task INotificationService.CreateNotificationAsync(EventType eventType, string comment)
    {
        return SaveNotificationAsync(eventType, comment);
    }

    public Task CreateNotificationAsync(EventType eventType, string comment, Guid entityId)
    {
        return SaveNotificationAsync(eventType, comment, entityId);
    }

    public Task CreateNotificationAsync(EventType eventType, string comment, string businessCode)
    {
        return SaveNotificationAsync(eventType, comment, businessCode: businessCode);
    }

    public Task CreateNotificationAsync(EventType eventType, string comment, Guid entityId, string businessCode)
    {
        return SaveNotificationAsync(eventType, comment, entityId, businessCode);
    }

    private async Task SaveNotificationAsync(EventType eventType, string comment, Guid? entityId = null, string? businessCode = null)
    {
        var @event = new Event
        {
            Type = eventType,
            Comment = comment,
            EntityId = entityId,
            BusinessCode = businessCode
        };
        
        dbContext.Events.Add(@event);
        await dbContext.SaveChangesAsync();
    } 

    public Task NotifySubscribersAsync(Event @event)
    {
        Console.WriteLine("TODO: Implement NotifySubscribers method");
        return Task.FromResult(0);
    }
}