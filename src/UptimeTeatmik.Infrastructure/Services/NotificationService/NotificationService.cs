using Hangfire;
using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Application.Common.Interfaces.NotificationService;
using UptimeTeatmik.Domain.Enums;
using UptimeTeatmik.Domain.Models;

namespace UptimeTeatmik.Infrastructure.Services.NotificationService;

public class NotificationService(IAppDbContext dbContext, IBackgroundJobClient backgroundJobClient, IEmailSender emailSender) : INotificationService
{
    public void CreateNotificationJob(EventType eventType, string comment, Guid? entityId = null, string? businessCode = null, List<string>? updatedParams = null)
    {
        backgroundJobClient.Enqueue(() => CreateNotificationAsync(eventType, comment, entityId, businessCode, updatedParams));
    }

    public async Task CreateNotificationAsync(EventType eventType, string comment, Guid? entityId = null, string? businessCode = null, List<string>? updatedParams = null)
    {
        var @event = await SaveNotificationAsync(eventType, comment, entityId, businessCode, updatedParams);
        await NotifySubscribersAsync(@event);
    }

    private async Task<Event> SaveNotificationAsync(EventType eventType, string comment, Guid? entityId = null, string? businessCode = null, List<string>? updatedParams = null)
    {
        Guid? id = null;
        if (businessCode != null || entityId != null)
        {
            id = entityId ?? await dbContext.Entities
                .Where(e => e.BusinessOrPersonalCode == businessCode)
                .Select(e => e.Id)
                .FirstOrDefaultAsync();
        }
        
        var @event = new Event
        {
            Type = eventType,
            Comment = comment,
            EntityId = id,
            UpdateParameters = updatedParams ?? []
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
            backgroundJobClient.Enqueue(() => SendEmailAsync(subscriber.SubscribersEmail, @event.Comment));
        }
    }

    private async Task<List<Subscription>> GetSubscribersAsync(Event @event)
    {
        return await dbContext.Subscriptions
            .Where(s => s.SubscribedBusinessId == @event.EntityId 
                        && (s.EventTypes.Contains(@event.Type)
                            || @event.UpdateParameters.Any(p => s.UpdateParameters.Contains(p))))
            .ToListAsync();
    }

    public async Task SendEmailAsync(string email, string body)
    {
        await emailSender.SendEmailAsync(email, $"New update", body);
    }

}