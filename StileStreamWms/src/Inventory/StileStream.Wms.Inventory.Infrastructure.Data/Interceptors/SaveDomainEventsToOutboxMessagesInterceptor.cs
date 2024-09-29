using Microsoft.EntityFrameworkCore.Diagnostics;

using Newtonsoft.Json;

using StileStream.Wms.Inventory.Infrastructure.Data.OutboxMessages.Entities;
using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Inventory.Infrastructure.Data.Interceptors;

public sealed class SaveDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA2326:Do not use TypeNameHandling values other than None", Justification = "From known source")]
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(eventData, nameof(eventData));
        var dbContext = eventData!.Context;
        if (dbContext == null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var domainEntities = dbContext.ChangeTracker.Entries<AggregateRoot>()
            .Where(x => x.Entity.GetDomainEvents().Any())
            .ToList();

        var domainEvents = domainEntities.SelectMany(x => x.Entity.GetDomainEvents()).ToList();

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        var outboxMessages = domainEvents.Select(domainEvent => 
            new OutboxMessage
            {
                Id = Guid.NewGuid(),
                TenantId = Guid.NewGuid(),
                Type = domainEvent.GetType().Name,
                Data = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }),
            })
            .ToList();
       
        dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
