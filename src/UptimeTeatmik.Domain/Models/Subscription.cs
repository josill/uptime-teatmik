using System.ComponentModel.DataAnnotations;
using UptimeTeatmik.Domain.Base;
using UptimeTeatmik.Domain.Enums;

namespace UptimeTeatmik.Domain.Models;

public class Subscription : BaseEntityMetadata
{
    [Key] public Guid Id { get; set; }
    public string SubscribersEmail { get; set; } = null!;
    public Guid? SubscribedBusinessId { get; set; }
    public EventType? EventType { get; set; }
    public List<string> UpdateParameters { get; set; } = [];
}