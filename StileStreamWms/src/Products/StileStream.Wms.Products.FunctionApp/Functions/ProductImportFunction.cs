using System.Threading;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using StileStream.Wms.Products.Application.Features.Products.CreateProducts.Contracts;
using StileStream.Wms.Products.Application.Features.Products.ImportProducts;
using StileStream.Wms.Products.Application.Features.Products.ImportProducts.Contracts;

namespace StileStream.Wms.Products.FunctionApp.Functions;

public class ProductImportFunction
{
    private readonly ILogger<ProductImportFunction> _logger;
    private readonly IMediator _mediator;

    public ProductImportFunction(ILogger<ProductImportFunction> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [Function(nameof(ProductImportFunction))]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route ="products/import")] HttpRequest req, CancellationToken cancellationToken)
    {
        if(req is null)
        {
            return new BadRequestObjectResult("Invalid request");
        }

        using var reader = new StreamReader(req.Body);
        var requestBody = await reader.ReadToEndAsync(cancellationToken);
        var productImportRequest = JsonConvert.DeserializeObject<ProductImportRequest>(requestBody);
        if (productImportRequest is null)
        {
            return new BadRequestObjectResult("Invalid request");
        }
        var command = new ImportProductsCommand(productImportRequest);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure
            ? new BadRequestObjectResult(result.Error)
            : new OkObjectResult(result.Value);
    }
}
