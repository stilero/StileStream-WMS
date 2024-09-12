using Shared.Domain.Interfaces;

namespace Shared.Domain.Events
{
    public interface IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }

        void ClearDomainEvents();
        void RaiseDomainEvent(IDomainEvent domainEvent);
    }
}