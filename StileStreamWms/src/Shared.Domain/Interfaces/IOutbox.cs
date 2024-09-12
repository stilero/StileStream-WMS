namespace Shared.Domain.Interfaces;

public interface IOutbox
{
    Task Add(IDomainEvent domainEvent);
}
