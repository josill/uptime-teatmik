using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Domain;
using UptimeTeatmik.Domain.Models;

namespace UptimeTeatmik.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Entity> Entities { get; set; }
    public DbSet<EntityOwner> EntityOwners { get; set; }
    public DbSet<Event> Events { get; set; }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Entity>()
    //         .Property(e => e.UniqueCode)
    //         .HasComputedColumnSql("COALESCE(\"FirstName\", '') || COALESCE(\"BusinessOrLastName\", '') || COALESCE(\"BusinessOrPersonalCode\", '')", stored: true);
    // }
}