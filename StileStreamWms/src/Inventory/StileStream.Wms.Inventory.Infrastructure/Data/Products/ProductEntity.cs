using StileStream.Wms.Inventory.Domain.Products.Entities;
using StileStream.Wms.SharedKernel.Infra.Data.EntityBase;

namespace StileStream.Wms.Inventory.Infrastructure.Data.Products;

public class ProductEntity : EntityBase
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Sku { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string Status { get; set; } = ProductStatus.Active;
}
