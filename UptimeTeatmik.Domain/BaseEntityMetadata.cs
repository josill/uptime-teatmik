namespace UptimeTeatmik.Domain;

public class BaseEntityMetadata
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = default;

}