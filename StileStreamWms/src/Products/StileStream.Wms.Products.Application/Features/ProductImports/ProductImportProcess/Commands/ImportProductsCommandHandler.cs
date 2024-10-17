using MassTransit;

using StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Errors;
using StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Interfaces;
using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;
using StileStream.Wms.SharedKernel.Application.Models.Results;
using StileStream.Wms.SharedKernel.Domain.Interfaces;

namespace StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Commands;
public sealed class ImportProductsCommandHandler : ICommandHandler<ImportProductsCommand, Guid>
{
    private readonly IProductImportService _productImportService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork _unitOfWork;

    public ImportProductsCommandHandler(IProductImportService productImportService, IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork)
    {
        _productImportService = productImportService;
        _publishEndpoint = publishEndpoint;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(ImportProductsCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return ImportProductsErrors.InvalidRequest;
        }

        var result = await _productImportService.CreateNew(request.Request, cancellationToken);
        await _publishEndpoint.Publish(new TestEvent(Guid.NewGuid(), "Test"), cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }
}

public record TestEvent(Guid Id, string Name);
