using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UptimeTeatmik.Infrastructure.Persistence;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var assemblyDirectory = Path.GetDirectoryName(typeof(AppDbContextFactory).Assembly.Location);
        var solutionDirectory = Directory.GetParent(assemblyDirectory ?? throw new InvalidOperationException("Unable to determine assembly directory."))?.Parent?.Parent?.Parent?.FullName;
        var appsettingsPath = Path.Combine(solutionDirectory ?? throw new InvalidOperationException($"Unable to determine solution directory, assembly directory is: {assemblyDirectory}"), "UptimeTeatmik.Api", "appsettings.json");
        var appsettingsDevelopmentPath = Path.Combine(solutionDirectory, "UptimeTeatmik.Api", "appsettings.Development.json");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(solutionDirectory)
            .AddJsonFile(appsettingsPath, optional: false)
            .AddJsonFile(appsettingsDevelopmentPath, optional: true)
            .AddEnvironmentVariables()
            .Build();
        
        var persistenceSettings = new PersistenceSettings();
        configuration.Bind(PersistenceSettings.SectionName, persistenceSettings);
        
        Console.WriteLine(persistenceSettings);
        
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