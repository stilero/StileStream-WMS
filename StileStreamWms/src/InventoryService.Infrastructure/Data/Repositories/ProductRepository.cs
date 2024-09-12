using InventoryService.Domain.Entities;
using InventoryService.Domain.Repositories;
using InventoryService.Infrastructure.Data.Entities.Extensions;

namespace InventoryService.Infrastructure.Data.Repositories;

public class ProductRepository(InventoryServiceDbContext dbContext) : IProductRepository
{
    private readonly InventoryServiceDbContext _dbContext = dbContext;

    public async Task Add(Product product, CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.AddAsync(product.ToEntity(), cancellationToken);
    }

    public async Task<Product?> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _dbContext.Products.FindAsync([id], cancellationToken);
        return product?.ToDomain() ?? null;
    }

    public void Update(Product product)
    {
        var productEntity = product.ToEntity();
        _dbContext.Products.Update(productEntity);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _dbContext.Products.FindAsync([id], cancellationToken);
        if (product is not null) _dbContext.Products.Remove(product);
    }
}
