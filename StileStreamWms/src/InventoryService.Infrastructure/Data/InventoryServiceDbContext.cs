using InventoryService.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Data;

public class InventoryServiceDbContext(DbContextOptions<InventoryServiceDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryServiceDbContext).Assembly);
    }
}

