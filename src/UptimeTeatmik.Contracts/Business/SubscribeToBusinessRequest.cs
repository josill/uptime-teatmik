using UptimeTeatmik.Domain.Enums;

namespace UptimeTeatmik.Contracts.Business;

public record SubscribeToBusinessRequest(
    string BusinessCode, 
    string SubscribersEmail, 
    List<EventType>? EventTypes = null, 
    List<string>? UpdateParameters = null);