using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;
using StileStream.Wms.SharedKernel.Domain.Models.Results;

using StileStream.Wms.Inventory.Application.Products.Errors;
using StileStream.Wms.Inventory.Application.Products.Responses;
using StileStream.Wms.Inventory.Domain.Products.Repositories;
using StileStream.Wms.Inventory.Domain.Products.Entities;

namespace StileStream.Wms.Inventory.Application.Products.Commands;
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
            return ProductError.InvalidRequest;
        }

        var products = request.Products.Select(p => Product.Load(
            id: Guid.NewGuid(),
            name: p.Name,
            sku: p.Sku,
            description: p.Description,
            manufacturer: p.Manufacturer,
            category: p.Category,
            status: p.Status,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow,
            createdBy: p.CreatedBy,
            updatedBy: p.UpdatedBy
            )).ToList();
        await _productRepository.AddRange(products, cancellationToken);
        return new ImportBulkProductResponse(products.Count);
    }
}
