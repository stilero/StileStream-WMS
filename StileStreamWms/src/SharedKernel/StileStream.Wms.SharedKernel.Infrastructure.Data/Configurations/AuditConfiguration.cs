using Microsoft.EntityFrameworkCore;

namespace StileStream.Wms.SharedKernel.Infrastructure.Data.Configurations;
public static class AuditConfiguration
{
    public const string CreatedOn = "CreatedOn";
    public const string UpdatedOn = "UpdatedOn";
    public const string CreatedBy = "CreatedBy";
    public const string UpdatedBy = "UpdatedBy";

    public static void Configure<TEntity>(ModelBuilder builder) where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        builder.Entity<TEntity>().Property<DateTime>(CreatedOn).HasDefaultValue(DateTime.UtcNow);
        builder.Entity<TEntity>().Property<DateTime>(UpdatedOn).HasDefaultValue(DateTime.UtcNow);
        builder.Entity<TEntity>().Property<string>(CreatedBy).HasMaxLength(100).HasDefaultValue("system");
        builder.Entity<TEntity>().Property<string>(UpdatedBy).HasMaxLength(100).HasDefaultValue("system");

    }
}
