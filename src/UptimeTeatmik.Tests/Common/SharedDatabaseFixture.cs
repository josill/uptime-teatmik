using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;
using UptimeTeatmik.Infrastructure.Persistence;

namespace UptimeTeatmik.Tests.Common;

public class SharedDatabaseFixture: IDisposable, IAsyncLifetime
{
    public Respawner Respawner { get; private set; }
    public PostgreSqlContainer DbContainer { get; } = new PostgreSqlBuilder()
        .WithImage("postgres:16")
        .WithDatabase("uptime-teatmik")
        .WithUsername("user")
        .WithPassword("postgres")
        .Build();

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public async Task InitializeAsync()
    {
        await DbContainer.StartAsync();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(DbContainer.GetConnectionString(), o =>
                o.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        
        await using (var connection = new NpgsqlConnection(DbContainer.GetConnectionString()))
        {
            await connection.OpenAsync();
            Respawner = await Respawner.CreateAsync(connection, new RespawnerOptions()
            {
                SchemasToInclude = new[] { "public" },
                DbAdapter = DbAdapter.Postgres
            });
        }
        
        await using (var context = new AppDbContext(optionsBuilder.Options))
        {
            await context.Database.MigrateAsync();
        }

    }

    public async Task DisposeAsync()
    {
        {
            await using var connection = new NpgsqlConnection(DbContainer.GetConnectionString());
            await connection.OpenAsync();
            
            await Respawner.ResetAsync(connection);
        }

        await DbContainer.StopAsync();
    }
}