using Xunit.Abstractions;

namespace UptimeTeatmik.Tests.Infrastructure.Tests.Abstractions;

[Collection("Database collection")]
public class IntegrationTestBase(IntegrationTestWebAppFactory factory, ITestOutputHelper testOutputHelper)
    : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly HttpClient HttpClient = factory.CreateClient();
    protected readonly ITestOutputHelper TestOutputHelper = testOutputHelper;
}