using UptimeTeatmik.Domain.Enums;

namespace UptimeTeatmik.Application.Common.Interfaces.NotificationService;

public interface INotificationService
{
    public void CreateNotificationJob(EventType eventType, string comment);
    public void CreateNotificationJob(EventType eventType, string comment, Guid entityId);
    public void CreateNotificationJob(EventType eventType, string comment, string businessCode);
    public void CreateNotificationJob(EventType eventType, string comment, Guid entityId, string businessCode);
    public void CreateNotificationJob(EventType eventType, string comment, Guid entityId, string businessCode, List<string> updatedParams);
}