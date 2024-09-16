using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Domain.Models;

namespace UptimeTeatmik.Infrastructure.Services.NotificationService;

public class NotificationService(IAppDbContext dbContext) : INotificationService
{
    public Task NotifySubscribers(Event @event)
    {
        Console.WriteLine("TODO: Implement NotifySubscribers method");
        return Task.FromResult(0);
    }
}