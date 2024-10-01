
using Microsoft.EntityFrameworkCore;

using StileStream.Wms.Products.Domain.Entities;
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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();
        UpdateShadowProperties();

        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateShadowProperties()
    {
        var entries = ChangeTracker.Entries().Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);

        foreach (var entry in entries)
        {
            if (entry.State is EntityState.Added)
            {
                if (entry.Property(AuditConfiguration.CreatedOn) != null)
                {
                    entry.Property(AuditConfiguration.CreatedOn).CurrentValue = DateTime.UtcNow;
                }

                if (entry.Property(AuditConfiguration.CreatedBy) != null)
                {
                    entry.Property(AuditConfiguration.CreatedBy).CurrentValue = "system";
                }

                if (entry.Property(SoftDeleteConfiguration.IsDeleted) != null)
                {
                    entry.Property(SoftDeleteConfiguration.IsDeleted).CurrentValue = false;
                }
            }

            if (entry.State is EntityState.Modified)
            {
                if (entry.Property(AuditConfiguration.UpdatedOn) != null)
                {
                    entry.Property(AuditConfiguration.UpdatedOn).CurrentValue = DateTime.UtcNow;
                }

                if (entry.Property(AuditConfiguration.UpdatedBy) != null)
                {
                    entry.Property(AuditConfiguration.UpdatedBy).CurrentValue = "system";
                }
            }
        }
    }
}

