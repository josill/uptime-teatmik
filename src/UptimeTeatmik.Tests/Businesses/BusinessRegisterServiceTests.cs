using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UptimeTeatmik.Domain.Models;
using UptimeTeatmik.Infrastructure.Persistence;
using UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;
using UptimeTeatmik.Infrastructure.Services.NotificationService;
using UptimeTeatmik.Infrastructure.Services.NotificationService.Senders.EmailSender;
using Xunit.Abstractions;

namespace UptimeTeatmik.Tests.Businesses;

public class BusinessRegisterServiceTests : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    private IConfiguration Configuration { get; }
    private BackgroundJobServer _backgroundJobServer;

    public BusinessRegisterServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        // Initialize Hangfire with MemoryStorage
        GlobalConfiguration.Configuration.UseMemoryStorage();
        _backgroundJobServer = new BackgroundJobServer();
    }

    public void Dispose()
    {
        _backgroundJobServer?.Dispose();
    }

    [Fact]
    public async Task AddEntity_ShouldAddToContext()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        await using var context = new AppDbContext(options);
        var httpClient = new HttpClient();
        
        var businessRegisterSettings = new BusinessRegisterSettings();
        Configuration.GetSection(BusinessRegisterSettings.SectionName).Bind(businessRegisterSettings);
        var businessRegisterSettingsOptions = Options.Create(businessRegisterSettings);

        var emailSettings = new EmailSenderSettings();
        Configuration.GetSection(EmailSenderSettings.SectionName).Bind(emailSettings);
        
        var businessRegisterBodyGenerator = new BusinessRegisterBodyGenerator(businessRegisterSettingsOptions);
        var notificationService = new NotificationService(context, new EmailSender(httpClient, emailSettings));
            
        var businessRegisterService = new BusinessRegisterService(context, httpClient, businessRegisterSettingsOptions, businessRegisterBodyGenerator, notificationService);
        var businessCodes = new List<string>() { "10308733" };

        // Act
        await businessRegisterService.UpdateBusinessesAsync(businessCodes);

        // Wait for background jobs to complete
        await Task.Delay(1000); // Adjust delay as needed

        _testOutputHelper.WriteLine(businessRegisterSettings.ChangesUrl);
        _testOutputHelper.WriteLine(businessRegisterSettings.DetailDataUrl);
        _testOutputHelper.WriteLine(businessRegisterSettings.Password);
        _testOutputHelper.WriteLine(businessRegisterSettings.Username);

        var id = Guid.NewGuid();
        var entity = new Entity()
        {
            Id = id,
            UniqueCode = id.ToString() 
        };

        context.Entities.Add(entity);
        await context.SaveChangesAsync();

        // Assert
        Assert.Single(context.Entities);
        Assert.Equal(entity.Id, context.Entities.Single().Id);

        // You may want to add assertions here to check if the background job was enqueued or executed
    }
}