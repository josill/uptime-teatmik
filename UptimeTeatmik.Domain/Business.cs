using System.ComponentModel.DataAnnotations;

namespace UptimeTeatmik.Domain;

public class Business : BaseEntityMetadata
{
    [Key] public Guid BusinessId { get; set; }
    [MaxLength(64)] public string BusinessCode { get; set; } = null!;
    [MaxLength(256)] public string? BusinessName { get; set; }
    public string? FormattedJson { get; set; }
}