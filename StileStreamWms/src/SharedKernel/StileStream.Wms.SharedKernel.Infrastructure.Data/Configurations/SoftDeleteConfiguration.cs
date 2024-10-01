using Microsoft.EntityFrameworkCore;

namespace StileStream.Wms.SharedKernel.Infrastructure.Data.Configurations;
public static class SoftDeleteConfiguration
{
    public const string IsDeleted = "IsDeleted";

    public static void Configure<TEntity>(ModelBuilder builder) where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        builder.Entity<TEntity>().Property<bool>(IsDeleted);
    }
}
