using StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Contracts;
using StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Interfaces;
using StileStream.Wms.Products.Application.Features.Products.Repositories;
using StileStream.Wms.Products.Domain.ProductImport;
using StileStream.Wms.Products.Domain.ProductImport.Entities;
using StileStream.Wms.SharedKernel.Application.Models.Results;

namespace StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Services;
public sealed class ProductImportService : IProductImportService
{
    private readonly IProductImportRepository _productImportRepository;
    private readonly IProductImportLineRepository _stagedProductDataRepository;
    private readonly IProductRepository _productRepository;

    public ProductImportService(IProductImportRepository productImportRepository, IProductImportLineRepository stagedProductDataRepository, IProductRepository productRepository)
    {
        _productImportRepository = productImportRepository;
        _stagedProductDataRepository = stagedProductDataRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<Guid>> CreateNew(ProductImportRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return ErrorResult.Validation("ProductImportErrors.InvalidRequest", "Request cannot be null");
        }

        var import = ProductImport.CreateNew(request.ImportType);
        var stagingData = request.Data.Select(d => ProductImportLine.CreateNew(
            d.Name, d.Sku, d.Description, d.Manufacturer, d.Category, d.Status, import)
            ).ToList();
        import.StageLines(stagingData);

        await _productImportRepository.AddAsync(import, cancellationToken);
        await _stagedProductDataRepository.AddRangeAsync(stagingData, cancellationToken);
        return import.Id;
    }

    public async Task<Result> ProcessImport(Guid importId, CancellationToken cancellationToken)
    {
        var import = await _productImportRepository.FindAsync(importId, cancellationToken);
        if (import is null)
        {
            return ErrorResult.NotFound("ProductImportErrors.ImportNotFound", "Import not found");
        }

        var products = import.ProcessLinesAndReturnProducts();
        _productImportRepository.Update(import);
        await _productRepository.AddRangeAsync(products, cancellationToken);
        return Result.Success();
    }
}
