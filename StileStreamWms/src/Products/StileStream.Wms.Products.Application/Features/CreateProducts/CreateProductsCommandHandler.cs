using StileStream.Wms.Products.ApiContracts.CreateProducts;
using StileStream.Wms.Products.Domain.Entities;
using StileStream.Wms.Products.Domain.Repositories;
using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;
using StileStream.Wms.SharedKernel.Domain.Models.Results;

namespace StileStream.Wms.Products.Application.Features.CreateProducts;

public sealed class CreateProductsCommandHandler(IProductRepository productRepository) : ICommandHandler<CreateProductsCommand, CreateProductsResponse>
{
    public async Task<Result<CreateProductsResponse>> Handle(CreateProductsCommand request, CancellationToken cancellationToken)
    {
        if (request is null || request.Products.Count == 0)
        {
            return ErrorResult.Validation("error", "Products cannot be null or empty");
        }

        var productEntities = new List<Product>();

        foreach (var product in request.Products)
        {
            productEntities.Add(Product.CreateNew(product.Name, product.Sku, product.Description, product.Manufacturer, product.Category));
        }

        await productRepository.AddRangeAsync(productEntities, cancellationToken);
        return new CreateProductsResponse(productEntities.Select(p => p.Id).ToList());
    }
}
