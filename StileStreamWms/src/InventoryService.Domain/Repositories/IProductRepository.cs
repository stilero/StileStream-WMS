using InventoryService.Domain.Entities;

namespace InventoryService.Domain.Repositories
{
    public interface IProductRepository
    {
        Task Add(Product product, CancellationToken cancellationToken = default);
        Task Delete(Guid id, CancellationToken cancellationToken = default);
        Task<Product?> Get(Guid id, CancellationToken cancellationToken = default);
        void Update(Product product);
    }
}