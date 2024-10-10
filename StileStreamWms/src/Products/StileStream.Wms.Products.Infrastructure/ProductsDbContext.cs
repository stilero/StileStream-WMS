
using Microsoft.EntityFrameworkCore;

using StileStream.Wms.Products.Domain.Aggregates;
using StileStream.Wms.SharedKernel.Infrastructure.Data.Configurations;
using StileStream.Wms.SharedKernel.Infrastructure.Data.Entities.OutboxMessages;

namespace StileStream.Wms.Products.Infrastructure;

public class ProductsDbContext : DbContext
{

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductsDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        ConfigureShadowProperties(modelBuilder);
    }

    private static void ConfigureShadowProperties(ModelBuilder modelBuilder)
    {
        AuditConfiguration.Configure<Product>(modelBuilder);
        TenantConfiguration.Configure<Product>(modelBuilder);
        SoftDeleteConfiguration.Configure<Product>(modelBuilder);
    }
 }

