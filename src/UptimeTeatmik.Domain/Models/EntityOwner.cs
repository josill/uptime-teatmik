using System.ComponentModel.DataAnnotations;
using UptimeTeatmik.Domain.Base;

namespace UptimeTeatmik.Domain.Models;

public class EntityOwner : BaseEntityMetadata
{
    [Key] public Guid Id { get; set; }
    [MaxLength(32)] public string? RoleInEntityAbbreviation { get; set; } = null!;
    [MaxLength(256)] public string? RoleInEntity { get; set; } = null!;
    
    public virtual Entity Owner { get; set; } = null!;
    public Guid OwnerId { get; set; }
    public virtual Entity Owned { get; set; } = null!;
    public Guid OwnedId { get; set; }
}