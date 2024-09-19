using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Microsoft.EntityFrameworkCore;
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
    public async Task CreateUpdateBusinessJob_ShouldBeEnqueued_WhenCreatingJob()
    {
        // Arrange
        var testBusinessCode = "10308733";
        var businessCodes = new List<string> { testBusinessCode };

        // Act
        await _testFactory.BusinessRegisterService.UpdateBusinessesAsync(businessCodes);
        var jobCreated = await CheckIfJobWasCreated();

        // Assert
        Assert.True(jobCreated != null, "Expected background job was not created");
    }

    [Fact]
    public async Task CreateEntity_ShouldCreate_WhenEntityDoesNotExist()
    {
        // Arrange
        var testBusinessCode = "10308733";

        // Act
        var createdEntity = await _testFactory.BusinessRegisterService.UpdateBusinessAsync(testBusinessCode);
        var entity = await _testFactory.DbContext.Entities
                .FirstOrDefaultAsync(e => e.BusinessOrPersonalCode == testBusinessCode);
        
        // Assert
        Assert.NotNull(entity);
    }
    
    [Fact]
    public async Task CreateEntity_ShouldNotUpdate_WhenCreatingAgain()
    {
        // Arrange
        var testBusinessCode = "10308733";

        // Act
        var createdEntity = await _testFactory.BusinessRegisterService.UpdateBusinessAsync(testBusinessCode);
        var createdEntityAgain = await _testFactory.BusinessRegisterService.UpdateBusinessAsync(testBusinessCode);
        var entity = await _testFactory.DbContext.Entities
            .FirstOrDefaultAsync(e => e.BusinessOrPersonalCode == testBusinessCode);
        
        // Assert
        Assert.NotNull(entity);
        Assert.NotNull(createdEntity);
        Assert.NotNull(createdEntity.FormattedJson);
        Assert.NotNull(createdEntityAgain);
        Assert.NotNull(createdEntityAgain.FormattedJson);
        Assert.Equal(createdEntity.FormattedJson, entity.FormattedJson);
    }

    private async Task<Job?> CheckIfJobWasCreated()
    {
        var jobs = JobStorage.Current.GetMonitoringApi();
        var enqueuedCount = jobs.EnqueuedJobs(EnqueuedState.DefaultQueue, 0, 1);

        return enqueuedCount.FirstOrDefault().Value.Job;
    }
}