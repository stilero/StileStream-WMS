using StileStream.Wms.Products.Application.Features.ImportProducts.Contracts;
using StileStream.Wms.Products.Domain.ProductImport;
using StileStream.Wms.Products.Domain.ProductImport.Entities;
using StileStream.Wms.Products.Domain.Products.Repositories;
using StileStream.Wms.SharedKernel.Application.Models.Results;

namespace StileStream.Wms.Products.Application.Features.ImportProducts.Services;
public sealed class ProductImportService : IProductImportService
{
    private readonly IProductRepository _productRepository;

    public ProductImportService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<Guid>> StartAsync(ProductImportRequest request, CancellationToken cancellationToken)
    {
        if(request is null)
        {
            return ErrorResult.Validation("ProductImportErrors.InvalidRequest", "Request cannot be null");
        }

        var import = ProductImport.CreateNew(request.ImportType);
        var stagingData = request.Data.Select(d => StagedProductData.CreateNew(
            d.Name, d.Sku, d.Description, d.Manufacturer, d.Category, d.Status)
            ).ToList();
        import.StageData(stagingData);
        import.ProcessStagedData();

    }
}


public interface IProductImportService
{
    Task<Guid> StartAsync(ProductImportRequest request, CancellationToken cancellationToken);
}
