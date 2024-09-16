using UptimeTeatmik.Domain.Enums;

namespace UptimeTeatmik.Application.Common.Interfaces;

public interface INotificationService
{
    public Task CreateNotificationAsync(EventType eventType, string comment);
    public Task CreateNotificationAsync(EventType eventType, string comment, Guid entityId);
    public Task CreateNotificationAsync(EventType eventType, string comment, string businessCode);
    public Task CreateNotificationAsync(EventType eventType, string comment, Guid entityId, string businessCode);
}