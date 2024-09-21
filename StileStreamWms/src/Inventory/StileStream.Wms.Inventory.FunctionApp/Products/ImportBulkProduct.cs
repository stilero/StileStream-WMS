using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using StileStream.Wms.Inventory.Application.Products.ProductImport.Commands;

namespace StileStream.Wms.Inventory.FunctionApp.Products;

public class ImportBulkProduct
{
    private readonly ILogger<ImportBulkProduct> _logger;
    private readonly IMediator _mediator;

    public ImportBulkProduct(ILogger<ImportBulkProduct> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [Function(nameof(ImportBulkProduct))]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "api/products/import")] HttpRequest req, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        if (req is null)
        {
            return new BadRequestObjectResult("Invalid request");
        }

        using var reader = new StreamReader(req.Body);
        var requestBody = await reader.ReadToEndAsync(cancellationToken);
        var command = JsonConvert.DeserializeObject<ImportBulkProductCommand>(requestBody);
        if (command is null)
        {
            return new BadRequestObjectResult("Invalid request");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure
            ? new BadRequestObjectResult(result.Error)
            : new OkObjectResult(result.Value);
    }
}
