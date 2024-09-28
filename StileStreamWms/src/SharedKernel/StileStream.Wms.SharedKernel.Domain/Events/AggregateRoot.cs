
using System.Text.Json.Serialization;

using StileStream.Wms.SharedKernel.Domain.Interfaces;

namespace StileStream.Wms.SharedKernel.Domain.Events;

public abstract class AggregateRoot : IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void AddDomainEvents(IEnumerable<IDomainEvent> domainEvents) => _domainEvents.AddRange(domainEvents);
    public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();
}
