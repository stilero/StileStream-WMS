using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;
using StileStream.Wms.SharedKernel.Domain.Models.Results;
using StileStream.Wms.Inventory.Domain.Products.Repositories;
using StileStream.Wms.Inventory.Domain.Products.Entities;
using StileStream.Wms.Inventory.Application.Products.ProductImport.Responses;
using StileStream.Wms.Inventory.Application.Products.ProductImport.Errors;

namespace StileStream.Wms.Inventory.Application.Products.ProductImport.Commands;
public sealed class ImportBulkProductCommandHandler : ICommandHandler<ImportBulkProductCommand, ImportBulkProductResponse>
{
    private readonly IProductRepository _productRepository;

    public ImportBulkProductCommandHandler(IProductRepository productRepository)
    {

        _productRepository = productRepository;
    }

    public async Task<Result<ImportBulkProductResponse>> Handle(ImportBulkProductCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return ProductImportError.InvalidRequest;
        }

        var productsResult = request.Products.Select(p => Product.CreateNew(
            name: p.Name,
            sku: p.Sku,
            description: p.Description,
            manufacturer: p.Manufacturer,
            category: p.Category
            )).ToList();
        if(productsResult.Any(p => p.IsFailure))
        {
            return productsResult.First(p => p.IsFailure).Error;
        }

        var products = productsResult.Select(p => p.Value).ToList();

        await _productRepository.AddRange(products, cancellationToken);
        return new ImportBulkProductResponse(productsResult.Count);
    }
}
