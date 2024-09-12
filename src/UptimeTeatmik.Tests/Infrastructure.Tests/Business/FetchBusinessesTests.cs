using System.Net;
using System.Net.Http.Json;
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
        var request = new UpdateBusinessesQuery(date);

        // Act
        var updateResponse = await HttpClient.PostAsJsonAsync("v3/auth/register", request);

        // Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var updateResponseBody = await updateResponse.Content.ReadAsStringAsync();
        var updateResponseData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UpdateBusinessesResult>>(updateResponseBody);

        Assert.NotNull(updateResponseData);
        Assert.Equal(162, updateResponseData.Count);
    }
}