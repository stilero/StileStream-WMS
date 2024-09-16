using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StileStream.Wms.Inventory.Infrastructure.Data.Entities;

namespace StileStream.Wms.Inventory.Infrastructure.Data.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Type).HasMaxLength(255).HasDefaultValue(string.Empty);
        builder.Property(p => p.Data).IsRequired().HasDefaultValue(string.Empty);
        builder.Property(p => p.OccurredOn).HasDefaultValue(DateTime.UtcNow);
        builder.Property(p => p.IsProcessed).HasDefaultValue(false);
        builder.Property(p => p.ProcessedOn).HasDefaultValue(DateTime.UtcNow);

        ConfigureIndexes(builder);

    }

    private static void ConfigureIndexes(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasIndex(p => p.Id).IsUnique();
        builder.HasIndex(p => p.IsProcessed);
    }
}
