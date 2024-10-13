using StileStream.Wms.Products.Application.Features.Products.ImportProducts.Interfaces;
using StileStream.Wms.Products.Domain.ProductImport;

namespace StileStream.Wms.Products.Infrastructure.ProductImports.Repositories;
public sealed class ProductImportRepository : IProductImportRepository
{
    public Task<Guid> AddAsync(ProductImport productImport, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task AddRangeAsync(IEnumerable<ProductImport> productImports, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<ProductImport> GetAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task Update(ProductImport productImport, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task UpdateRange(IEnumerable<ProductImport> productImports, CancellationToken cancellationToken) => throw new NotImplementedException();
}
