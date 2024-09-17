using StileStream.Wms.Inventory.Domain.Products.Entities;

namespace StileStream.Wms.Inventory.Infrastructure.Data.Entities;

public class ProductEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Sku { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string Status { get; set; } = ProductStatus.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = "system";
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string UpdatedBy { get; set; } = "system";
}
