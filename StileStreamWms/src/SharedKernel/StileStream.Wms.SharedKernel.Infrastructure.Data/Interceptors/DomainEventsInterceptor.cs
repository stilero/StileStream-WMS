using MassTransit;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.SharedKernel.Infrastructure.Data.Interceptors;

/// <summary>
/// Interceptor to dispatch domain events to MassTransit.
/// </summary>
public class DomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IPublishEndpoint _publishEndpoint;

    public DomainEventsInterceptor(IPublishEndpoint publishEndpoint)
    {
        ArgumentNullException.ThrowIfNull(publishEndpoint, nameof(publishEndpoint));
        _publishEndpoint = publishEndpoint;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ArgumentNullException.ThrowIfNull(eventData, nameof(eventData));
        DispatchDomainEvents(eventData.Context!);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(eventData, nameof(eventData));
        DispatchDomainEvents(eventData.Context!);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void DispatchDomainEvents(DbContext dbContext)
    {
        var domainEntities = dbContext.ChangeTracker.Entries<AggregateRoot>()
            .Where(x => x.Entity.GetDomainEvents().Any())
            .ToList();

        var domainEvents = domainEntities.SelectMany(x => x.Entity.GetDomainEvents()).ToList();

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            _publishEndpoint.Publish(domainEvent);
        }       
    }
}
