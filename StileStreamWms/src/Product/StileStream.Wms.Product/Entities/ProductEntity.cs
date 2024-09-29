using StileStream.Wms.Product.Events;
using StileStream.Wms.SharedKernel.Domain.Models.Results;
using StileStream.Wms.SharedKernel.Domain.Primitives;
using StileStream.Wms.SharedKernel.Infrastructure.Data.Interfaces;

namespace StileStream.Wms.Product.Entities;

public sealed class ProductEntity : AggregateRoot, IAuditable, ISoftDeleteable
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Sku { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Manufacturer { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public string Status { get; private set; } = ProductStatus.Active;
    public DateTime CreatedAt { get; set ; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? UpdatedBy { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public ProductEntity()
    {
    }

    public static Result<ProductEntity> CreateNew(string name, string sku, string description, string manufacturer, string category)
    {
        var product = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
            Sku = sku,
            Manufacturer = manufacturer,
            Status = ProductStatus.Active,
            Description = description,
            Category = category
        };

        product.RaiseDomainEvent(new ProductCreatedEvent(product));

        return product;
    }

    public static Result<ProductEntity> Update(Guid id, string name, string sku, string description, string category)
    {
        var product = new ProductEntity
        {
            Id = id,
            Name = name,
            Sku = sku,
            Description = description,
            Category = category,
        };

        product.RaiseDomainEvent(new ProductUpdatedEvent(product));

        return product;
    }

    public static ProductEntity Load(Guid id, string name, string sku, string description, string manufacturer, string category, string status) => new()
    {
        Id = id,
        Name = name,
        Sku = sku,
        Description = description,
        Manufacturer = manufacturer,
        Category = category,
        Status = status
    };
}
