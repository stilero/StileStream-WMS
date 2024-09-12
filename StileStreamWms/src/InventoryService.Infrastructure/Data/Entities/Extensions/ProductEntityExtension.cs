using InventoryService.Domain.Entities;

namespace InventoryService.Infrastructure.Data.Entities.Extensions;

public static class ProductEntityExtension
{
    public static Product ToDomain(this ProductEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Sku = entity.Sku,
        Description = entity.Description,
        Category = entity.Category,
        Status = entity.Status,
        CreatedAt = entity.CreatedAt,
        CreatedBy = entity.CreatedBy,
        UpdatedAt = entity.UpdatedAt,
        UpdatedBy = entity.UpdatedBy
    };

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
