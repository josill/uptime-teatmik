using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Application.Common;
using UptimeTeatmik.Domain;

namespace UptimeTeatmik.Infrastructure;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<BusinessPerson> BusinessPerson { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}