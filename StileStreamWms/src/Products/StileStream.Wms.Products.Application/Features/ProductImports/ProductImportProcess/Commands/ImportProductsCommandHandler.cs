using MassTransit;

using StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Errors;
using StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Interfaces;
using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;
using StileStream.Wms.SharedKernel.Application.Models.Results;

namespace StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Commands;
public sealed class ImportProductsCommandHandler : ICommandHandler<ImportProductsCommand, Guid>
{
    private readonly IProductImportService _productImportService;

    public ImportProductsCommandHandler(IProductImportService productImportService)
    {
        _productImportService = productImportService;
    }

    public async Task<Result<Guid>> Handle(ImportProductsCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return ImportProductsErrors.InvalidRequest;
        }

        var result = await _productImportService.CreateNew(request.Request, cancellationToken);
        return result;
    }
}

public record TestEvent(Guid Id, string Name);
