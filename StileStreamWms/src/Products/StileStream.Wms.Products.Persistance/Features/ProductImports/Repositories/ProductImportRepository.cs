using StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Interfaces;
using StileStream.Wms.Products.Domain.ProductImport;
using StileStream.Wms.Products.Persistance;

namespace StileStream.Wms.Products.Persistance.Features.ProductImports.Repositories;
public sealed class ProductImportRepository(ProductsDbContext dbContext) : IProductImportRepository
{
    public async Task AddAsync(ProductImport productImport, CancellationToken cancellationToken) => await dbContext.AddAsync(productImport, cancellationToken);
    public async Task AddRangeAsync(IEnumerable<ProductImport> productImports, CancellationToken cancellationToken) => await dbContext.AddRangeAsync(productImports, cancellationToken);
    public async Task<ProductImport?> FindAsync(Guid id, CancellationToken cancellationToken) => await dbContext.FindAsync<ProductImport>(id, cancellationToken);
    public void Update(ProductImport productImport) => dbContext.Update(productImport);
    public void UpdateRange(IEnumerable<ProductImport> productImports) => dbContext.UpdateRange(productImports);
}
