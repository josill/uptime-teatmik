using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;
using UptimeTeatmik.Infrastructure.Persistence;

namespace UptimeTeatmik.Tests.Abstractions;

public class IntegrationTestFactory : IAsyncLifetime
{
    public IServiceProvider Services { get; private set; }

    public IntegrationTestFactory()
    {
        var services = new ServiceCollection();

        services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(_dbContainer.GetConnectionString()));

        Services = services.BuildServiceProvider();
    }
    
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