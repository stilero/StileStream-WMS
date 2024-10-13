using StileStream.Wms.Products.Application.Features.Products.ImportProducts.Interfaces;
using StileStream.Wms.Products.Domain.ProductImport.Entities;

namespace StileStream.Wms.Products.Infrastructure.ProductImports;
public sealed class ProductImportLineRepository : IProductImportLineRepository
{
    public Task AddAsync(ProductImportLine stagedProductData, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task AddRangeAsync(IEnumerable<ProductImportLine> stagedProductData, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<ProductImportLine> GetAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task Update(ProductImportLine stagedProductData, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task UpdateRange(IEnumerable<ProductImportLine> stagedProductData, CancellationToken cancellationToken) => throw new NotImplementedException();
}
