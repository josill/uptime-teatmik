using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UptimeTeatmik.Infrastructure.Persistence;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName ?? throw new InvalidOperationException("appsettings.json file path is invalid in AppDbContextFactory.cs setup"))
            .AddJsonFile("UptimeTeatmik.Api/appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        
        var persistenceSettings = new PersistenceSettings();
        configuration.GetSection(PersistenceSettings.SectionName).Bind(persistenceSettings);

        var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
        var username = Environment.GetEnvironmentVariable("DB_USER") ?? "default_user";
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "default_password";
        var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "default_database";

        var connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database}";
   
        
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql<AppDbContext>(connectionString);
        
        return new AppDbContext(optionsBuilder.Options);
    }
}