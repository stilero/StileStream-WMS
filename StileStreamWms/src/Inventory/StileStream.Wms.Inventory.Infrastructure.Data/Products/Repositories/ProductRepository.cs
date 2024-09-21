using StileStream.Wms.Inventory.Domain.Products.Entities;
using StileStream.Wms.Inventory.Domain.Products.Repositories;
using StileStream.Wms.Inventory.Infrastructure.Data.Products.Extensions;
using StileStream.Wms.SharedKernel.Domain.Interfaces;

namespace StileStream.Wms.Inventory.Infrastructure.Data.Products.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly InventoryServiceDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public ProductRepository(InventoryServiceDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
         

    public async Task Add(Product product, CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.AddAsync(product.ToEntity(), cancellationToken);
        _unitOfWork.TrackEntity(product);
    }
        

    public async Task AddRange(IEnumerable<Product> products, CancellationToken cancellationToken = default)
    {
        var productEntities = products.Select(p => p.ToEntity()).ToList();
        await _dbContext.Products.AddRangeAsync(productEntities, cancellationToken);
        _unitOfWork.TranckEntities(products);
    }

    public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _dbContext.Products.FindAsync([id], cancellationToken);
        return product?.ToDomain();
    }

    public void Update(Product product)
    {
        _dbContext.Products.Update(product.ToEntity());
        _unitOfWork.TrackEntity(product);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _dbContext.Products.FindAsync([id], cancellationToken);
        if (product is not null)
        {
            _dbContext.Products.Remove(product);
            _unitOfWork.TrackEntity(product.ToDomain());
        }
    }
}
