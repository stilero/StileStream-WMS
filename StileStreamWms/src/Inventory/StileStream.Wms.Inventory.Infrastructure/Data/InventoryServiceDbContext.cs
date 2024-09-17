using Microsoft.EntityFrameworkCore;

using StileStream.Wms.Inventory.Infrastructure.Data.Entities;

namespace StileStream.Wms.Inventory.Infrastructure.Data;

public class InventoryServiceDbContext : DbContext
{
    public InventoryServiceDbContext(DbContextOptions<InventoryServiceDbContext> options) : base(options)
    {
    }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryServiceDbContext).Assembly);
    }
}

