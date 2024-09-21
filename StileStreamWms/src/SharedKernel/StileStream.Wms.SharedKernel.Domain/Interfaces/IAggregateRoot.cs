namespace StileStream.Wms.SharedKernel.Domain.Interfaces
{
    public interface IAggregateRoot
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }

        void ClearDomainEvents();
        void RaiseDomainEvent(IDomainEvent domainEvent);
    }
}
