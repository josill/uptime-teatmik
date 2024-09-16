using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Domain.Models;
using UptimeTeatmik.Infrastructure.Persistence;

namespace UptimeTeatmik.Tests.Businesses;

public class BusinessRegisterServiceTests
{
    [Fact]
    public async Task AddEntity_ShouldAddToContext()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        await using var context = new AppDbContext(options);
        var id = Guid.NewGuid();
        var entity = new Entity()
        {
            Id = id,
            UniqueCode = id.ToString() 
        };

        // Act
        context.Entities.Add(entity);
        await context.SaveChangesAsync();

        // Assert
        Assert.Single(context.Entities);
        Assert.Equal(entity.Id, context.Entities.Single().Id);
    }
}