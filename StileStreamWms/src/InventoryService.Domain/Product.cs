using InventoryService.Domain.Events;
using Shared.Domain.Events;
using Shared.Domain.Models.Results;

namespace InventoryService.Domain;

public class Product : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Sku { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public ProductStatus Status { get; set; } = ProductStatus.Active;
    public DateTime? CreatedAt { get; private set; }
    public string CreatedBy { get; private set; } = string.Empty;
    public DateTime? UpdatedAt { get; private set; }
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

        product.RaiseDomainEvent(new ProductCreatedDomainEvent(product));

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

        product.RaiseDomainEvent(new ProductUpdatedDomainEvent(product));

        return product;
    }    
}
