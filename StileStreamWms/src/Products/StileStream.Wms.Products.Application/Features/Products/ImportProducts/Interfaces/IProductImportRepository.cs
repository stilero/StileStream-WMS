using StileStream.Wms.Products.Domain.ProductImport;

namespace StileStream.Wms.Products.Application.Features.Products.ImportProducts.Interfaces;
public interface IProductImportRepository
{
    Task AddAsync(ProductImport productImport, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<ProductImport> productImports, CancellationToken cancellationToken);
    void Update(ProductImport productImport);
    void UpdateRange(IEnumerable<ProductImport> productImports);
    Task<ProductImport?> FindAsync(Guid id, CancellationToken cancellationToken);

}
