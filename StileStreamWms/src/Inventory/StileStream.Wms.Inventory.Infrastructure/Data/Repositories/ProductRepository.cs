using StileStream.Wms.Inventory.Domain.Entities;
using StileStream.Wms.Inventory.Domain.Repositories;

using StileStream.Wms.Inventory.Infrastructure.Data.Entities.Extensions;

namespace StileStream.Wms.Inventory.Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly InventoryServiceDbContext _dbContext;

   public ProductRepository(InventoryServiceDbContext dbContext) =>
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task Add(Product product, CancellationToken cancellationToken = default) =>
        await _dbContext.Products.AddAsync(product.ToEntity(), cancellationToken);

    public async Task AddRange(IEnumerable<Product> products, CancellationToken cancellationToken = default)
    {
        var productEntities = products.Select(p => p.ToEntity());
        await _dbContext.Products.AddRangeAsync(productEntities, cancellationToken);
    }

    public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default)
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
        if (product is not null)
        {
            _dbContext.Products.Remove(product);
        }
    }
}
