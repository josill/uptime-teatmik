using System.ComponentModel.DataAnnotations;
using UptimeTeatmik.Domain.Base;
using UptimeTeatmik.Domain.Enums;

namespace UptimeTeatmik.Domain.Models;

public class Event : BaseEntityMetadata
{
    [Key] public Guid Id { get; set; }
    public Guid? EntityId { get; set; }
    public EventType Type { get; set; }
    public string Comment { get; set; } = null!;
    public List<string>  UpdateParameters { get; set; } = [];
}