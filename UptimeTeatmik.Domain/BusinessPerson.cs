using System.ComponentModel.DataAnnotations;

namespace UptimeTeatmik.Domain;

public class BusinessPerson : BaseEntityMetadata
{
    [Key] public Guid PersonInBusinessId { get; set; }
    public Guid PersonId { get; set; }
    public Guid BusinessId { get; set; }
}