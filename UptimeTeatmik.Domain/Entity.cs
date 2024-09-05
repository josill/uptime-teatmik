using System.ComponentModel.DataAnnotations;

namespace UptimeTeatmik.Domain;

public class Entity : BaseEntityMetadata
{
    [Key] public Guid Id { get; set; }
    [MaxLength(64)] public string BusinessOrPersonalCode { get; set; } = null!;
    [MaxLength(128)] public string? FirstName { get; set; } 
    [MaxLength(256)] public string? BusinessOrLastName { get; set; }
    [MaxLength(32)] public string? EntityTypeAbbreviation { get; set; }
    [MaxLength(256)] public string? EntityType { get; set; }
    public string? FormattedJson { get; set; }
}