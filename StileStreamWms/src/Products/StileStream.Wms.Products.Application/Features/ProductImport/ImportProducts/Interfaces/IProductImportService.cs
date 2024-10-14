using StileStream.Wms.Products.Application.Features.ProductImport.ImportProducts.Contracts;
using StileStream.Wms.SharedKernel.Application.Models.Results;

namespace StileStream.Wms.Products.Application.Features.ProductImport.ImportProducts.Interfaces;

public interface IProductImportService
{
    Task<Result<Guid>> CreateNew(ProductImportRequest request, CancellationToken cancellationToken);
    Task<Result> ProcessImport(Guid importId, CancellationToken cancellationToken);
}
