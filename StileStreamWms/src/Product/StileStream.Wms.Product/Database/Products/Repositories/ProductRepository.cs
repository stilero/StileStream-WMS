using StileStream.Wms.Product.Entities;
using StileStream.Wms.Product.Interfaces;
using StileStream.Wms.SharedKernel.Domain.Interfaces;

namespace StileStream.Wms.Product.Database.Products.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public ProductRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    public async Task Add(ProductEntity product, CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.AddAsync(product, cancellationToken);
    }


    public async Task AddRange(IEnumerable<ProductEntity> products, CancellationToken cancellationToken = default)
    {       
        await _dbContext.Products.AddRangeAsync(products, cancellationToken);
    }

    public async Task<ProductEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default) => await _dbContext.Products.FindAsync([id], cancellationToken);

    public void Update(ProductEntity product)
    {
        _dbContext.Products.Update(product);
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
