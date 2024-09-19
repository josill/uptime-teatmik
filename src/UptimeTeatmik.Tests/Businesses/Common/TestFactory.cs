using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using UptimeTeatmik.Infrastructure.Persistence;
using UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;
using UptimeTeatmik.Infrastructure.Services.NotificationService;
using UptimeTeatmik.Infrastructure.Services.NotificationService.Senders.EmailSender;

namespace UptimeTeatmik.Tests.Businesses.Common;

public class TestFactory : IDisposable, IAsyncDisposable
{
    public IConfiguration Configuration { get; }
    public Mock<IBackgroundJobClient> BackgroundJobClientMock { get; }
    public AppDbContext DbContext { get; }
    public BusinessRegisterService BusinessRegisterService { get; }
    public Mock<HttpMessageHandler> HttpMessageHandlerMock { get; }

    public TestFactory()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        // Initialize Hangfire with MemoryStorage
        GlobalConfiguration.Configuration.UseMemoryStorage();
        BackgroundJobClientMock = new Mock<IBackgroundJobClient>();

        // Set up in-memory database
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique name for each test run
            .Options;
        DbContext = new AppDbContext(options);

        // Set up mocked HttpClient
        HttpMessageHandlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(HttpMessageHandlerMock.Object);

        // Set up services
        var businessRegisterSettings = new BusinessRegisterSettings();
        Configuration.GetSection(BusinessRegisterSettings.SectionName).Bind(businessRegisterSettings);
        var businessRegisterSettingsOptions = Options.Create(businessRegisterSettings);

        var emailSettings = new EmailSenderSettings();
        Configuration.GetSection(EmailSenderSettings.SectionName).Bind(emailSettings);
        
        var businessRegisterBodyGenerator = new BusinessRegisterBodyGenerator(businessRegisterSettingsOptions);
        var notificationService = new NotificationService(DbContext, new EmailSender(httpClient, emailSettings));
            
        BusinessRegisterService = new BusinessRegisterService(
            DbContext, 
            httpClient, 
            businessRegisterSettingsOptions, 
            businessRegisterBodyGenerator, 
            notificationService
        );
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await DbContext.DisposeAsync();
    }
}