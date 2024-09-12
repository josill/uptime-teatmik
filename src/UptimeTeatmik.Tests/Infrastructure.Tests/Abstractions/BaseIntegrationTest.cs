using Microsoft.Extensions.DependencyInjection;
using UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;
using UptimeTeatmik.Infrastructure.Persistence;
using UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;

namespace UptimeTeatmik.Tests.Infrastructure.Tests.Abstractions;

public class BaseIntegrationTest : IClassFixture<IntegrationInfrastructureTestFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    protected IBusinessRegisterService BusinessRegisterService { get; }
    protected AppDbContext DbContext { get; }
    // protected Faker Faker { get; }

    protected BaseIntegrationTest(IntegrationInfrastructureTestFactory factory)
    {
        _scope = factory.Services.CreateScope();
        BusinessRegisterService = _scope.ServiceProvider.GetRequiredService<BusinessRegisterService>();
        DbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Faker = new Faker();
    }
    
    public void Dispose()
    {
        _scope.Dispose();
    }
}