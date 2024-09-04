using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UptimeTeatmik.Infrastructure.Persistence;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var persistenceSettings = new PersistenceSettings();
        configuration.GetSection(PersistenceSettings.SectionName).Bind(persistenceSettings);

        var host = Environment.GetEnvironmentVariable("DB_HOST") ?? persistenceSettings.Host;
        var port = Environment.GetEnvironmentVariable("DB_PORT") ?? persistenceSettings.Port.ToString();
        var username = Environment.GetEnvironmentVariable("DB_USER") ?? persistenceSettings.Username;
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? persistenceSettings.Password;
        var database = Environment.GetEnvironmentVariable("DB_NAME") ?? persistenceSettings.Database;

        var connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database}";
   
        
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql<AppDbContext>(connectionString);
        
        return new AppDbContext(optionsBuilder.Options);
    }
}