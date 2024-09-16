using Hangfire;
using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Domain.Enums;
using UptimeTeatmik.Domain.Models;

namespace UptimeTeatmik.Infrastructure.Services.NotificationService;

public class NotificationService(IAppDbContext dbContext) : INotificationService
{
    public async Task CreateNotificationAsync(EventType eventType, string comment)
    {
        var @event = await SaveNotificationAsync(eventType, comment);
        await NotifySubscribersAsync(@event);
    }

    public async Task CreateNotificationAsync(EventType eventType, string comment, Guid entityId)
    {
        var @event = await SaveNotificationAsync(eventType, comment, entityId);
        await NotifySubscribersAsync(@event);
    }

    public async Task CreateNotificationAsync(EventType eventType, string comment, string businessCode)
    {
        var @event = await SaveNotificationAsync(eventType, comment, businessCode: businessCode);
        await NotifySubscribersAsync(@event);
    }

    public async Task CreateNotificationAsync(EventType eventType, string comment, Guid entityId, string businessCode)
    {
        var @event = await SaveNotificationAsync(eventType, comment, entityId, businessCode);
        await NotifySubscribersAsync(@event);
    }
    
    private async Task<Event> SaveNotificationAsync(EventType eventType, string comment, Guid? entityId = null, string? businessCode = null)
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

        return @event;
    } 

    private async Task NotifySubscribersAsync(Event @event)
    {
        var subscribers = await GetSubscribersAsync(@event);
        foreach (var subscriber in subscribers)
        {
            BackgroundJob.Enqueue(() => SendEmailAsync(subscriber.SubscribersEmail));
        }
    }

    private async Task<List<Subscription>> GetSubscribersAsync(Event @event)
    {
        return await dbContext.Subscriptions
            .Where(s => s.SubscribedBusinessId == @event.EntityId
            && s.EventType == @event.Type)
            .ToListAsync();

        // matchingEventsQuery
        //     .Where(s => s.EventType == @event.Type
        //                 || s.UpdateParameters == @event.UpdatedParameters);
    }

    private async Task SendEmailAsync(string email)
    {
        Console.WriteLine("TODO: send email to the subscriber");
    }
}