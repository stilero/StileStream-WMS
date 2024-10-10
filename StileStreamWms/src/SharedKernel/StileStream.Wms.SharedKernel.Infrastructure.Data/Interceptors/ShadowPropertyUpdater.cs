using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using StileStream.Wms.SharedKernel.Infrastructure.Data.Configurations;

namespace StileStream.Wms.SharedKernel.Infrastructure.Data.Interceptors;

//TODO: MAKE THIS an interceptor
public static class ShadowPropertyUpdater
{

    public static void UpdateShadowProperties(ChangeTracker changeTracker)
    {
        var entries = changeTracker.Entries().Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);

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
