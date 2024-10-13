using StileStream.Wms.Products.Domain.ProductImport;

namespace StileStream.Wms.Products.Application.Features.Products.ImportProducts.Interfaces;
public interface IProductImportRepository
{
    Task<Guid> AddAsync(ProductImport productImport, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<ProductImport> productImports, CancellationToken cancellationToken);
    Task Update(ProductImport productImport, CancellationToken cancellationToken);
    Task UpdateRange(IEnumerable<ProductImport> productImports, CancellationToken cancellationToken);
    Task<ProductImport> GetAsync(Guid id, CancellationToken cancellationToken);

}
