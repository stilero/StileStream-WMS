using StileStream.Wms.Products.Application.Features.Products.Repositories;
using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;
using StileStream.Wms.SharedKernel.Application.Models.Results;

namespace StileStream.Wms.Products.Application.Features.Products.DeleteProduct;
public sealed class DeleteProductCommandHandler
    : ICommandHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
        => _productRepository = productRepository;

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return ErrorResult.Validation("DeleteProductError.InvalidRequest", "Request is null");
        }

        var product = await _productRepository.FindAsync(request.Id, cancellationToken);
        if (product is null)
        {
            return ErrorResult.NotFound("DeleteProductError.NotFound", "Product not found");
        }

        product.Delete();
        await _productRepository.Delete(request.Id, cancellationToken);
        return Result.Success();
    }
}
