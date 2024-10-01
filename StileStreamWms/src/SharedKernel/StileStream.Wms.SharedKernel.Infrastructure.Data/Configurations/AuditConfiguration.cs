using Microsoft.EntityFrameworkCore;

namespace StileStream.Wms.SharedKernel.Infrastructure.Data.Configurations;
public static class AuditConfiguration
{
    public static void Configure<TEntity>(ModelBuilder builder) where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        builder.Entity<TEntity>().Property<DateTime>("CreatedOn").HasDefaultValue(DateTime.UtcNow);
        builder.Entity<TEntity>().Property<DateTime>("UpdatedOn").HasDefaultValue(DateTime.UtcNow);
        builder.Entity<TEntity>().Property<string>("CreatedBy").HasDefaultValue("system");
        builder.Entity<TEntity>().Property<string>("UpdatedBy").HasDefaultValue("system");

    }
}
