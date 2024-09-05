namespace UptimeTeatmik.Infrastructure.Persistence;

public class PersistenceSettings
{
    public const string SectionName = "DefaultConnection";
    public string Host { get; init; } = null!;
    public string Port { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Database { get; init; } = null!;
}