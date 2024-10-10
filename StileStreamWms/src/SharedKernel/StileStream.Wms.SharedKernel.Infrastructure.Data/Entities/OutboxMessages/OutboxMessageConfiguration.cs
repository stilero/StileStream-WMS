using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StileStream.Wms.SharedKernel.Infrastructure.Data.Entities.OutboxMessages;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
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
        builder.HasIndex(p => p.TenantId);
        builder.HasIndex(p => p.CorrelationId);
    }
}
