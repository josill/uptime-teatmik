using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Npgsql;
using UptimeTeatmik.Application.Common;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Infrastructure.Persistence;
using UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;

namespace UptimeTeatmik.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager builderConfiguration
    )
    {
        services
            .AddHttpClient()
            .AddBusinessRegisterService(builderConfiguration)
            .AddPersistence(builderConfiguration)
            .AddHangfireServices(builderConfiguration);
        
        return services;
    }
    
    private static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var persistenceSettings = new PersistenceSettings();
        configuration.Bind(PersistenceSettings.SectionName, persistenceSettings);
        
        var host = Environment.GetEnvironmentVariable("DB_HOST") ?? persistenceSettings.Host;
        var port = Environment.GetEnvironmentVariable("DB_PORT") ?? persistenceSettings.Port.ToString();
        var username = Environment.GetEnvironmentVariable("DB_USER") ?? persistenceSettings.Username;
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? persistenceSettings.Password;
        var database = Environment.GetEnvironmentVariable("DB_NAME") ?? persistenceSettings.Database;

        var connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database}";
    
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(dataSource, o =>
            {
                o.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
            }));
        
        services.AddScoped<IAppDbContext, AppDbContext>();
        
        return services;
    }
    
    private static IServiceCollection AddHangfireServices(
        this IServiceCollection services,
        IConfiguration builderConfiguration
    )
    {
        var persistenceSettings = new PersistenceSettings();
        builderConfiguration.Bind(PersistenceSettings.SectionName, persistenceSettings);
        
        var host = Environment.GetEnvironmentVariable("DB_HOST") ?? persistenceSettings.Host;
        var port = Environment.GetEnvironmentVariable("DB_PORT") ?? persistenceSettings.Port.ToString();
        var username = Environment.GetEnvironmentVariable("DB_USER") ?? persistenceSettings.Username;
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? persistenceSettings.Password;
        var database = Environment.GetEnvironmentVariable("DB_NAME") ?? persistenceSettings.Database;

        var connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database}";
        // var connectionString =
        //     "Host=localhost;Port=5432;Username=josill;Password=uptime_teatmik;Database=uptime-teatmik";

        services.AddHangfire((_, configuration) => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(c =>
                c.UseNpgsqlConnection(connectionString))
        );
        
        services.AddHangfireServer();

        // Add framework services.
        services.AddMvc();
        
        return services;
    }

    public static IServiceCollection AddBusinessRegisterService(
        this IServiceCollection services,
        IConfiguration builderConfiguration
    )
    {
        var businessRegisterSettings = new BusinessRegisterSettings();
        builderConfiguration.Bind(BusinessRegisterSettings.SectionName, businessRegisterSettings);
        services.AddSingleton(Options.Create(businessRegisterSettings));
        services.AddScoped<IBusinessRegisterService, BusinessRegisterService>();

        return services;
    }
}