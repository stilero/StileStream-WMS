namespace StileStream.Wms.SharedKernel.Domain.Interfaces
{
    public interface IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }

        void ClearDomainEvents();
        void RaiseDomainEvent(IDomainEvent domainEvent);
    }
}