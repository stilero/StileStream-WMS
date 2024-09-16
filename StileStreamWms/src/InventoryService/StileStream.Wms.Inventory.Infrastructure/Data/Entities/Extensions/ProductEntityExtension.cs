using InventoryService.Domain.Entities;

namespace StileStream.Wms.Inventory.Infrastructure.Data.Entities.Extensions;

public static class ProductEntityExtension
{
    public static Product ToDomain(this ProductEntity entity) =>
        Product.Load(
            id: entity.Id,
            name: entity.Name,
            sku: entity.Sku,
            description: entity.Description,
            manufacturer: entity.Manufacturer,
            category: entity.Category,
            status: entity.Status,
            createdAt: entity.CreatedAt,
            createdBy: entity.CreatedBy,
            updatedAt: entity.UpdatedAt,
            updatedBy: entity.UpdatedBy);

    public static ProductEntity ToEntity(this Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Sku = product.Sku,
        Description = product.Description,
        Category = product.Category,
        Status = product.Status,
        CreatedAt = product.CreatedAt,
        CreatedBy = product.CreatedBy,
        UpdatedAt = product.UpdatedAt,
        UpdatedBy = product.UpdatedBy
    };
}
