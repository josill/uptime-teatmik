using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Domain.Models;

namespace UptimeTeatmik.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Entity> Entities { get; set; }
    public virtual DbSet<EntityOwner> EntityOwners { get; set; }
    public virtual DbSet<Event> Events { get; set; }
}