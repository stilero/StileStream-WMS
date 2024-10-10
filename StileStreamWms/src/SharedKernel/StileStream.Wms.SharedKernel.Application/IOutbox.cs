namespace StileStream.Wms.SharedKernel.Domain.Interfaces;

public interface IOutbox
{
    Task Add(IDomainEvent domainEvent);
}
