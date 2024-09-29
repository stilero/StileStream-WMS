using StileStream.Wms.Product.Entities;

namespace StileStream.Wms.Product.Interfaces;

public interface IProductRepository
{
    Task Add(ProductEntity product, CancellationToken cancellationToken = default);
    Task AddRange(IEnumerable<ProductEntity> products, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
    Task<ProductEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    void Update(ProductEntity product);
}
