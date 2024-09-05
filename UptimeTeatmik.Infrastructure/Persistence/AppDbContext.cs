using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Application.Common;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Domain;

namespace UptimeTeatmik.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<BusinessRegisterEntity> BusinessRegisterEntities { get; set; }
}