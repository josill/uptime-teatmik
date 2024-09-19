using Hangfire;
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
    public async Task CreateEntity_ShouldSucceed_WhenEntityDoesNotExist()
    {
        // Arrange
        var testBusinessCode = "10308733";
        var businessCodes = new List<string> { testBusinessCode };

        // Act
        await _testFactory.BusinessRegisterService.UpdateBusinessesAsync(businessCodes);
        // await Task.Delay(10000);
        var jobCreated = await CheckIfJobWasCreated(testBusinessCode);
        await WaitForJobCompletion(testBusinessCode, TimeSpan.FromSeconds(10));
        var createdBusiness = await _testFactory.DbContext.Entities
            .FirstOrDefaultAsync();
            // .FirstOrDefaultAsync(e => e.BusinessOrPersonalCode == testBusinessCode);


            
        // Assert
        Assert.True(jobCreated, "Expected background job was not created");
        // Assert.NotNull(createdBusiness);
        // Assert.Single(_testFactory.DbContext.Entities);
        // Assert.Equal(createdBusiness.BusinessOrPersonalCode, createdBusiness.BusinessOrPersonalCode);

        // You may want to add assertions here to check if the background job was enqueued or executed
    }
    
    public async Task<bool> CheckIfJobWasCreated(string businessCode)
    {
        var jobs = JobStorage.Current.GetMonitoringApi();
        var enqueuedCount = jobs.EnqueuedCount(EnqueuedState.DefaultQueue);
        var servers = jobs.Servers();
        _testOutputHelper.WriteLine($"Number of servers: {servers.Count}");
        _testOutputHelper.WriteLine(enqueuedCount.ToString());
        return enqueuedCount > 0;
    }
    
    private async Task WaitForJobCompletion(string jobId, TimeSpan timeout, int delayInMs = 500)
    {
        var jobs = JobStorage.Current.GetMonitoringApi();

        var startTime = DateTime.UtcNow;
        while (DateTime.UtcNow - startTime < timeout)
        {
            var enqueued = jobs.EnqueuedJobs(EnqueuedState.DefaultQueue, 0, 50);
            var job = enqueued.FirstOrDefault();
            _testOutputHelper.WriteLine(job.ToString());
            var state = job.Value.State;
            _testOutputHelper.WriteLine("state");
            _testOutputHelper.WriteLine(state);
            
            switch (state)
            {
                case "Succeeded":
                    return;
                case "Failed":
                    throw new Exception($"Job {jobId} failed");
                default:
                    await Task.Delay(delayInMs); 
                    break;
            }
        }
        throw new TimeoutException($"Job {jobId} did not complete within the specified timeout");
    }
}