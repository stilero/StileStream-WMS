using StileStream.Wms.Inventory.Domain.Products.Entities;
using StileStream.Wms.SharedKernel.Infrastructure.Data.Interfaces;

namespace StileStream.Wms.Inventory.Infrastructure.Data.Products;

public class ProductEntity : Product, IAuditable, ISoftDeleteable
{
    public Guid TenantId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
