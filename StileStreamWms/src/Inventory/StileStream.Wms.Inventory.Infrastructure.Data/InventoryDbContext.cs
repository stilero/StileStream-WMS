using Microsoft.EntityFrameworkCore;

using StileStream.Wms.Inventory.Infrastructure.Data.OutboxMessages.Entities;
using StileStream.Wms.Inventory.Infrastructure.Data.Products;

namespace StileStream.Wms.Inventory.Infrastructure.Data;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
    {
    }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
    }
}

