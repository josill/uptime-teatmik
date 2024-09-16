using UptimeTeatmik.Domain.Models;

namespace UptimeTeatmik.Application.Common.Interfaces;

public interface INotificationService
{
    public Task NotifySubscribers(Event @event);
}