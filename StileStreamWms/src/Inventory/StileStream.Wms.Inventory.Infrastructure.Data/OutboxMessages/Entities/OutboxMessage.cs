namespace StileStream.Wms.Inventory.Infrastructure.Data.OutboxMessages.Entities;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
    public bool IsProcessed { get; set; }
    public DateTime ProcessedOn { get; set; }
}
