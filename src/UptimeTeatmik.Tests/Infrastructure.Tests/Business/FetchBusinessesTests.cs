using System.Net;
using UptimeTeatmik.Application.Businesses.Queries.UpdatesBusinesses;
using UptimeTeatmik.Tests.Infrastructure.Tests.Abstractions;
using Xunit.Abstractions;
using FluentAssertions;

namespace UptimeTeatmik.Tests.Infrastructure.Tests.Business;

public class FetchBusinessesTests(IntegrationTestWebAppFactory factory, ITestOutputHelper testOutputHelper) : IntegrationTestBase(factory, testOutputHelper)
{
    [Fact]
    public async Task Should_FetchUpdatedBusinesses_WhenDateIsValid()
    {
        // Arrange
        var date = new DateTime(2000, 6, 30);
        var isoDate = date.ToString("o");

        // Act
        var updateResponse = await HttpClient.GetAsync($"v1/businesses/updates?date={isoDate}");

        // Assert
        var updateResponseBody = await updateResponse.Content.ReadAsStringAsync();
        var updateResponseData = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateBusinessesResult>(updateResponseBody);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        Assert.NotNull(updateResponseData);
        Assert.Equal(162, updateResponseData.AmountOfBusinessesUpdated);
    }
    
    [Fact]
    public async Task ShouldNot_FetchUpdatedBusinesses_WhenDateIsMissing()
    {
        // Arrange

        // Act
        var updateResponse = await HttpClient.GetAsync($"v1/businesses/updates");

        // Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task ShouldNot_FetchUpdatedBusinesses_WhenDateIsInvalid()
    {
        // Arrange
        var date = new DateTime(2000, 6, 30);

        // Act
        var updateResponse = await HttpClient.GetAsync($"v1/businesses/updates?date={date}");

        // Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}