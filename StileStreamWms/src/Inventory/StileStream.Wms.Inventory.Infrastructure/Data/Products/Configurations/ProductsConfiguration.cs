using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StileStream.Wms.Inventory.Domain.Products.Entities;

namespace StileStream.Wms.Inventory.Infrastructure.Data.Products.Configurations;

public class ProductsConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Description).HasMaxLength(255).HasDefaultValue(string.Empty);
        builder.Property(p => p.Category).HasMaxLength(50).HasDefaultValue(string.Empty);
        builder.Property(p => p.Status).HasMaxLength(50).HasDefaultValue(ProductStatus.Active);
        builder.Property(p => p.Sku).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

        ConfigureIndexes(builder);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasIndex(p => p.Sku).IsUnique();
        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.Category);
        builder.HasIndex(p => p.Status);
    }
}
