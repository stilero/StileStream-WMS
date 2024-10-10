using Microsoft.EntityFrameworkCore;

namespace StileStream.Wms.SharedKernel.Infrastructure.Data.Configurations;
public static class TenantConfiguration
{
    public const string TenantId = "TenantId";
    public static void Configure<TEntity>(ModelBuilder builder) where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        builder.Entity<TEntity>().Property<Guid>(TenantId);
    }
}
