using Microsoft.EntityFrameworkCore;

using StileStream.Wms.Inventory.Infrastructure.Data.OutboxMessages.Entities;
using StileStream.Wms.Inventory.Infrastructure.Data.Products;
using StileStream.Wms.SharedKernel.Infrastructure.Data.EntityBase;

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
        ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryServiceDbContext).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityBase).Assembly);
    }
}

