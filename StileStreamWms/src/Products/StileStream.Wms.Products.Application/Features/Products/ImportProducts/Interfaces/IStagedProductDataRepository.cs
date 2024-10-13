using StileStream.Wms.Products.Domain.ProductImport.Entities;

namespace StileStream.Wms.Products.Application.Features.Products.ImportProducts.Interfaces;
public interface IStagedProductDataRepository
{
    Task AddAsync(StagedProductData stagedProductData, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<StagedProductData> stagedProductData, CancellationToken cancellationToken);
    Task Update(StagedProductData stagedProductData, CancellationToken cancellationToken);
    Task UpdateRange(IEnumerable<StagedProductData> stagedProductData, CancellationToken cancellationToken);
    Task<StagedProductData> GetAsync(Guid id, CancellationToken cancellationToken);
}
