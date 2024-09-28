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

    private ProductEntity()
    {
    }

    private ProductEntity(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Sku = product.Sku;
        Description = product.Description;
        Manufacturer = product.Manufacturer;
        Category = product.Category;
        Status = product.Status;

        AddDomainEvents(product.DomainEvents);
    }

    public static ProductEntity FromProduct(Product product) => new(product);

    public Product ToDomain() => Product.Load(Id, Name, Sku, Description, Manufacturer, Category, Status);
}
