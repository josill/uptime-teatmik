using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Domain;

namespace UptimeTeatmik.Application.Common;

public interface IAppDbContext
{
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<BusinessPerson> BusinessPerson { get; set; }
}