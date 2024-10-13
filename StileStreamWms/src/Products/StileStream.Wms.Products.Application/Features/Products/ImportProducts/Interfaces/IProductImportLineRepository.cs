using StileStream.Wms.Products.Domain.ProductImport.Entities;

namespace StileStream.Wms.Products.Application.Features.Products.ImportProducts.Interfaces;
public interface IProductImportLineRepository
{
    Task AddAsync(ProductImportLine stagedProductData, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<ProductImportLine> stagedProductData, CancellationToken cancellationToken);
    Task Update(ProductImportLine stagedProductData, CancellationToken cancellationToken);
    Task UpdateRange(IEnumerable<ProductImportLine> stagedProductData, CancellationToken cancellationToken);
    Task<ProductImportLine> GetAsync(Guid id, CancellationToken cancellationToken);
}
