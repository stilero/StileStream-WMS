using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using StileStream.Wms.SharedKernel.Infrastructure.Data.Configurations;

namespace StileStream.Wms.SharedKernel.Infrastructure.Data.Interceptors;
public class AuditSaveChangesInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ArgumentNullException.ThrowIfNull(eventData, nameof(eventData));

        UpdateAuditFields(eventData.Context!);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(eventData, nameof(eventData));

        UpdateAuditFields(eventData.Context!);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateAuditFields(DbContext context)
    {
        if (context == null)
        {
            return;
        }

        var entries = context.ChangeTracker.Entries();
        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if(entry.Property(AuditConfiguration.CreatedOn) != null )
                    {
                        entry.Property(AuditConfiguration.CreatedOn).CurrentValue = now;
                    }

                    if (entry.Property(AuditConfiguration.UpdatedOn) != null)
                    {
                        entry.Property(AuditConfiguration.UpdatedOn).CurrentValue = now;
                    }

                    break;
                case EntityState.Modified:
                    if (entry.Property(AuditConfiguration.UpdatedOn) != null)
                    {
                        entry.Property(AuditConfiguration.UpdatedOn).CurrentValue = now;
                    }
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    break;
            }
        }
    }
}
