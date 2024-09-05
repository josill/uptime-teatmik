using System.ComponentModel.DataAnnotations;

namespace UptimeTeatmik.Domain;

public class Person : BaseEntityMetadata
{
    [Key] public Guid PersonId { get; set; }
    [MaxLength(128)] public string? FirstName { get; set; }
    [MaxLength(128)] public string? LastName { get; set; }
    [MaxLength(254)] public string? BusinessName { get; set; }
    [MaxLength(64)] public string? PersonalCode { get; set; }
    [MaxLength(64)] public string? BusinessCode { get; set; } 
    public PersonType PersonType { get; set; }    
    public string? FormattedJson { get; set; }
}