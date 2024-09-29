using System.Collections.ObjectModel;

using Carter;

using MediatR;

using StileStream.Wms.Product.Contracts;
using StileStream.Wms.Product.Entities;
using StileStream.Wms.Product.Interfaces;
using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;
using StileStream.Wms.SharedKernel.Domain.Models.Results;

namespace StileStream.Wms.Product.Features;

public static class CreateProducts
{
    public sealed record Command(IReadOnlyCollection<CreateProductRequest> Products) : ICommand<BulkCreateProductResponse>;

    internal sealed class Handler(IProductRepository productRepository) : ICommandHandler<Command, BulkCreateProductResponse>
    {
        async Task<Result<BulkCreateProductResponse>> IRequestHandler<Command, Result<BulkCreateProductResponse>>.Handle(Command request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return ProductImportError.InvalidRequest;
            }

            var productsResult = request.Products.Select(p => ProductEntity.CreateNew(
                        name: p.Name,
                        sku: p.Sku,
                        description: p.Description,
                        manufacturer: p.Manufacturer,
                        category: p.Category
                        )).ToList();

            if (productsResult.Any(p => p.IsFailure))
            {
                return productsResult.First(p => p.IsFailure).Error;
            }

            var products = productsResult.Select(p => p.Value).ToList();

            await productRepository.AddRange(products, cancellationToken);
            return new BulkCreateProductResponse(products.Select(p => p.Id).ToList().AsReadOnly());
        }
    }

    internal static class ProductImportError
    {
        public static ErrorResult InvalidRequest => ErrorResult.Validation("ProductError.InvalidRequest", "Invalid Request");
    }
}
public class CreateProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/products", async (IMediator mediator, CreateProducts.Command command) =>
        {
            var result = await mediator.Send(command);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
            .WithName("CreateProducts")
            .WithOpenApi();
    }
}
