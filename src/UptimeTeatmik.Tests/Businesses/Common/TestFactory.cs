using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UptimeTeatmik.Infrastructure.Persistence;
using UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;
using UptimeTeatmik.Infrastructure.Services.NotificationService;
using UptimeTeatmik.Infrastructure.Services.NotificationService.Senders.EmailSender;

namespace UptimeTeatmik.Tests.Businesses.Common;

public class TestFactory : IDisposable
{
    public IConfiguration Configuration { get; }
    private readonly BackgroundJobServer _backgroundJobServer;
    public AppDbContext Context { get; }
    public BusinessRegisterService BusinessRegisterService { get; }

    public TestFactory()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        // Initialize Hangfire with MemoryStorage
        GlobalConfiguration.Configuration.UseMemoryStorage();
        _backgroundJobServer = new BackgroundJobServer();

        // Set up in-memory database
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        Context = new AppDbContext(options);

        // Set up services
        var httpClient = new HttpClient();
        
        var businessRegisterSettings = new BusinessRegisterSettings();
        Configuration.GetSection(BusinessRegisterSettings.SectionName).Bind(businessRegisterSettings);
        var businessRegisterSettingsOptions = Options.Create(businessRegisterSettings);

        var emailSettings = new EmailSenderSettings();
        Configuration.GetSection(EmailSenderSettings.SectionName).Bind(emailSettings);
        
        var businessRegisterBodyGenerator = new BusinessRegisterBodyGenerator(businessRegisterSettingsOptions);
        var notificationService = new NotificationService(Context, new EmailSender(httpClient, emailSettings));
            
        BusinessRegisterService = new BusinessRegisterService(Context, httpClient, businessRegisterSettingsOptions, businessRegisterBodyGenerator, notificationService);
    }

    public void Dispose()
    {
        _backgroundJobServer?.Dispose();
        Context?.Dispose();
    }
}