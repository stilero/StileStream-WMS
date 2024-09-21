using MediatR;

using StileStream.Wms.SharedKernel.Domain.Events;

namespace StileStream.Wms.SharedKernel.Application.EventDispatchers;
public static class DomainEventDispatcher
{
    public static async Task DispatchDomainEventsAsync(IEnumerable<AggregateRoot> aggregates, IMediator mediator, CancellationToken cancellationToken)
    {
        var domainEvents = aggregates.SelectMany(aggregate => aggregate.DomainEvents).ToList();
       

        var tasks = domainEvents.Select(async domainEvent =>
        {
            await mediator.Publish(domainEvent, cancellationToken);
        });

        await Task.WhenAll(tasks);
        aggregates.ToList().ForEach(entity => entity.ClearDomainEvents());
    }
}
