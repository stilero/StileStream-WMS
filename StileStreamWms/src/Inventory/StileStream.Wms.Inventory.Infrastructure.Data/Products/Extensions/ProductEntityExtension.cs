using StileStream.Wms.Inventory.Domain.Products.Entities;

namespace StileStream.Wms.Inventory.Infrastructure.Data.Products.Extensions;

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
            status: entity.Status);

    public static ProductEntity ToEntity(this Product product) => 
        (ProductEntity)Product.Load(product.Id, product.Name, product.Sku, product.Description, product.Manufacturer, product.Category, product.Status);
}
