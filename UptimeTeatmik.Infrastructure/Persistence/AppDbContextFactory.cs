using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UptimeTeatmik.Infrastructure;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql<AppDbContext>(
            "Host=localhost;Port=5432;Username=josill;Password=uptime_teatmik;Database=uptime-teatmik"
        );
        
        return new AppDbContext(optionsBuilder.Options);
    }
}