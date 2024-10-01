using StileStream.Wms.Products.Domain.Enums;
using StileStream.Wms.SharedKernel.Infrastructure.Data.EntityBase;

namespace StileStream.Wms.Products.Infrastructure.Entities.Products;

public class ProductEntity : EntityBase
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Sku { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public ProductStatus Status { get; set; } = ProductStatus.Active;
}
