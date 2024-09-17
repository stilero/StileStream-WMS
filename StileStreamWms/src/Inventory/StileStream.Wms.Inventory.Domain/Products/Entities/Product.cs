using StileStream.Wms.Inventory.Domain.Products.Events;
using StileStream.Wms.SharedKernel.Domain.Events;
using StileStream.Wms.SharedKernel.Domain.Models.Results;

namespace StileStream.Wms.Inventory.Domain.Products.Entities;

public class Product : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Sku { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Manufacturer { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public string Status { get; private set; } = ProductStatus.Active;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public string CreatedBy { get; private set; } = string.Empty;
    public string UpdatedBy { get; private set; } = string.Empty;

    public static Result<Product> Create(string name, string sku, string description, string category, string? createdBy = null)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Sku = sku,
            Description = description,
            Category = category,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy ?? "system"
        };

        product.RaiseDomainEvent(new ProductCreatedEvent(product));

        return product;
    }

    public static Result<Product> Update(Guid id, string name, string sku, string description, string category, string? updatedBy = null)
    {
        var product = new Product
        {
            Id = id,
            Name = name,
            Sku = sku,
            Description = description,
            Category = category,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = updatedBy ?? "system"
        };

        product.RaiseDomainEvent(new ProductUpdatedEvent(product));

        return product;
    }

    public static Product Load(Guid id, string name, string sku, string description, string manufacturer, string category, string status, DateTime createdAt, DateTime updatedAt, string createdBy, string updatedBy) => new()
    {
        Id = id,
        Name = name,
        Sku = sku,
        Description = description,
        Manufacturer = manufacturer,
        Category = category,
        Status = status,
        CreatedAt = createdAt,
        UpdatedAt = updatedAt,
        CreatedBy = createdBy,
        UpdatedBy = updatedBy
    };
}
