
using Microsoft.EntityFrameworkCore;

using StileStream.Wms.Products.Application.Repositories;
using StileStream.Wms.Products.Domain.Products;

namespace StileStream.Wms.Products.Infrastructure.Repositories;

public class ProductRepository(ProductsDbContext productsDbContext) : IProductRepository
{
    private readonly ProductsDbContext _dbContext = productsDbContext;

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        => await _dbContext.Products.AddAsync(product, cancellationToken);

    public async Task AddRangeAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default)
        => await _dbContext.Products.AddRangeAsync(products, cancellationToken);

    public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default) 
        => await _dbContext.Products.FindAsync([id], cancellationToken);

    public void Update(Product product) 
        => _dbContext.Products.Update(product);

    public async Task Delete(Guid id, CancellationToken cancellationToken = default) 
        => await _dbContext.Products.Where(p => p.Id == id).ExecuteDeleteAsync(cancellationToken);    
}
