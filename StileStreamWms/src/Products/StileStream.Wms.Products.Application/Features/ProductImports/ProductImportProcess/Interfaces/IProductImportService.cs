using StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Contracts;
using StileStream.Wms.SharedKernel.Application.Models.Results;

namespace StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Interfaces;

public interface IProductImportService
{
    Task<Result<Guid>> CreateNew(ProductImportRequest request, CancellationToken cancellationToken);
    Task<Result> ProcessImport(Guid importId, CancellationToken cancellationToken);
}
