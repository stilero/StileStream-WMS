namespace StileStream.Wms.SharedKernel.Domain.Interfaces;

public interface IDomainEvent
{
    //Suggest the most common properties used for domain events
    public Guid Id { get; }
    public DateTime OccurredOn { get; }
    public Guid AggregateId { get; }
    public string AggregateType { get; }
    public string EventType { get; }
}
