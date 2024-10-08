namespace StileStream.Wms.SharedKernel.Infrastructure.Data.Entities.OutboxMessages;

public class OutboxMessage
{
    //TODO: Add EntityId property
    //TODO: Add entity type property
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string CorrelationId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public string Properties { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
    public bool IsProcessed { get; set; }
    public DateTime ProcessedOn { get; set; }
}
