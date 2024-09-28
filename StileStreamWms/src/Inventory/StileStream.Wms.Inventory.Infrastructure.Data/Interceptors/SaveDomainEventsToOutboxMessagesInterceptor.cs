using System.Security.Cryptography.X509Certificates;

using Microsoft.EntityFrameworkCore.Diagnostics;

using Newtonsoft.Json;

using StileStream.Wms.Inventory.Infrastructure.Data.OutboxMessages.Entities;
using StileStream.Wms.SharedKernel.Domain.Events;

namespace StileStream.Wms.Inventory.Infrastructure.Data.Interceptors;

public sealed class SaveDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData!.Context;
        if (dbContext == null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var domainEntities = dbContext.ChangeTracker.Entries<AggregateRoot>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();

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
        
        //var events = dbContext.ChangeTracker.Entries<AggregateRoot>()
        //    .Select(x => x.Entity)
        //    .SelectMany(aggregateRoot =>
        //    {
        //        var domainEvents = aggregateRoot.DomainEvents;
        //        aggregateRoot.ClearDomainEvents();
        //        return domainEvents;
        //    })
        //    .Select(domainEvent => 
        //    new OutboxMessage
        //    {
        //        Id = Guid.NewGuid(),
        //        TenantId = Guid.NewGuid(),
        //        Type = domainEvent.GetType().Name,
        //        Data = JsonConvert.SerializeObject(
        //            domainEvent,
        //            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }),
        //    })
        //    .ToList();

        dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
