
using MassTransit;

using Microsoft.EntityFrameworkCore;

using StileStream.Wms.Products.Domain.ProductImport;
using StileStream.Wms.Products.Domain.ProductImport.Entities;
using StileStream.Wms.Products.Domain.Products;
using StileStream.Wms.SharedKernel.Infrastructure.Data.Configurations;

namespace StileStream.Wms.Products.Persistance;

public class ProductsDbContext : DbContext
{

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImport> ProductImports { get; set; }
    public DbSet<ProductImportLine> ProductImportLines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductsDbContext).Assembly);
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        //modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        ConfigureShadowProperties(modelBuilder);
    }

    private static void ConfigureShadowProperties(ModelBuilder modelBuilder)
    {
        AuditConfiguration.Configure<Product>(modelBuilder);
        AuditConfiguration.Configure<ProductImport>(modelBuilder);
        AuditConfiguration.Configure<ProductImportLine>(modelBuilder);

        TenantConfiguration.Configure<Product>(modelBuilder);
        TenantConfiguration.Configure<ProductImport>(modelBuilder);
        TenantConfiguration.Configure<ProductImportLine>(modelBuilder);

        SoftDeleteConfiguration.Configure<Product>(modelBuilder);
    }
}

