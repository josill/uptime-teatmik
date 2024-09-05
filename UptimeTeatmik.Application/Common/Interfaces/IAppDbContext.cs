using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Domain;

namespace UptimeTeatmik.Application.Common.Interfaces;

public interface IAppDbContext
{
    public DbSet<Entity> Entities { get; set; }
    public DbSet<EntityOwner> EntityOwners { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}