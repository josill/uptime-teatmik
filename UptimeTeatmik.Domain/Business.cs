using System.ComponentModel.DataAnnotations;

namespace UptimeTeatmik.Domain;

public class Business : BaseEntityMetadata
{
    [Key] public Guid BusinessId { get; set; }
    public Guid BusinessName { get; set; }
    public Guid BusinessCode { get; set; }
}