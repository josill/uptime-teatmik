using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Domain.Models;

namespace UptimeTeatmik.Application.Common.Interfaces;

public interface IAppDbContext
{
    public DbSet<Entity> Entities { get; set; }
    public DbSet<EntityOwner> EntityOwners { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}