using System.ComponentModel.DataAnnotations;

namespace UptimeTeatmik.Domain;

public class Person : BaseEntityMetadata
{
    [Key] public Guid PersonId { get; set; }
    [MaxLength(128)]
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public PersonType PersonType { get; set; }    
}