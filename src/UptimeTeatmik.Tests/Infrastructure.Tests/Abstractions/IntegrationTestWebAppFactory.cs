using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UptimeTeatmik.Infrastructure.Persistence;
using UptimeTeatmik.Tests.Common;

namespace UptimeTeatmik.Tests.Infrastructure.Tests.Abstractions;

public class IntegrationTestWebAppFactory(SharedDatabaseFixture dbFixture) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the default context configuration
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));

            // Use shared container's connection string
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(dbFixture.DbContainer.GetConnectionString(), o =>
                {
                    o.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                }));
        });
    }
    
}