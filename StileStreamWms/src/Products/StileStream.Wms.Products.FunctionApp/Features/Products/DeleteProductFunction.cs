using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

using StileStream.Wms.Products.Application.Features.Products.DeleteProduct;

namespace StileStream.Wms.Products.FunctionApp.Features.Products;

public class DeleteProductFunction
{
    private readonly ILogger<DeleteProductFunction> _logger;
    private readonly IMediator _mediator;

    public DeleteProductFunction(ILogger<DeleteProductFunction> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [Function(nameof(DeleteProductFunction))]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "products/{id:guid}")] HttpRequest req,
        Guid id,
        CancellationToken cancellationToken)
    {
        if (req is null)
        {
            return new BadRequestObjectResult("Invalid request");
        }

        var command = new DeleteProductCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure
            ? new BadRequestObjectResult(result.Error)
            : new OkResult();
    }
}
