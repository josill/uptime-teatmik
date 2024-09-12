using Testcontainers.PostgreSql;

namespace UptimeTeatmik.Tests.Abstractions;

public class IntegrationTestFactory : IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16")
        .WithDatabase("uptime-teatmik")
        .WithUsername("user")
        .WithPassword("postgres")
        .Build();

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}