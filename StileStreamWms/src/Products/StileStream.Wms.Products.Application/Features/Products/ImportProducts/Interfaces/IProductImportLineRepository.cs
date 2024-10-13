using StileStream.Wms.Products.Domain.ProductImport.Entities;

namespace StileStream.Wms.Products.Application.Features.Products.ImportProducts.Interfaces;
public interface IProductImportLineRepository
{
    Task AddAsync(ProductImportLine productLine, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<ProductImportLine> productLines, CancellationToken cancellationToken);
    void Update(ProductImportLine productLine);
    void UpdateRange(IEnumerable<ProductImportLine> productLines);
    Task<ProductImportLine?> FindAsync(Guid id, CancellationToken cancellationToken);
}
