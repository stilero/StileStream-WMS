using StileStream.Wms.Products.Domain.Products;

namespace StileStream.Wms.Products.Domain.Products.Repositories;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
    Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    void Update(Product product);
}
