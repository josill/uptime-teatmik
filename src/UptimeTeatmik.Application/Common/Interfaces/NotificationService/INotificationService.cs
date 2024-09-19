using UptimeTeatmik.Domain.Enums;

namespace UptimeTeatmik.Application.Common.Interfaces.NotificationService;

public interface INotificationService
{
    void CreateNotificationJob(EventType eventType, string comment, Guid? entityId = null, string? businessCode = null, List<string>? updatedParams = null);
}