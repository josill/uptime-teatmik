using Microsoft.Extensions.Configuration;
using UptimeTeatmik.Domain.Models;
using UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;
using UptimeTeatmik.Tests.Businesses.Common;
using Xunit.Abstractions;

namespace UptimeTeatmik.Tests.Businesses;

public class BusinessRegisterServiceTests : IClassFixture<TestFactory>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly TestFactory _testFactory;

    public BusinessRegisterServiceTests(TestFactory testFactory, ITestOutputHelper testOutputHelper)
    {
        _testFactory = testFactory;
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task AddEntity_ShouldAddToContext()
    {
        // Arrange
        var businessCodes = new List<string>() { "10308733" };

        // Act
        await _testFactory.BusinessRegisterService.UpdateBusinessesAsync(businessCodes);

        var businessRegisterSettings = new BusinessRegisterSettings();
        _testFactory.Configuration.GetSection(BusinessRegisterSettings.SectionName).Bind(businessRegisterSettings);

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

        _testFactory.Context.Entities.Add(entity);
        await _testFactory.Context.SaveChangesAsync();

        // Assert
        Assert.Single(_testFactory.Context.Entities);
        Assert.Equal(entity.Id, _testFactory.Context.Entities.Single().Id);

        // You may want to add assertions here to check if the background job was enqueued or executed
    }
}