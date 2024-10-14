using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using StileStream.Wms.Products.Application.Features.Products.CreateProducts;
using StileStream.Wms.Products.Application.Features.Products.CreateProducts.Contracts;

namespace StileStream.Wms.Products.FunctionApp.Features.Products;

public class CreateProductsFunction
{
    private readonly ILogger<CreateProductsFunction> _logger;
    private readonly IMediator _mediator;

    public CreateProductsFunction(ILogger<CreateProductsFunction> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [Function(nameof(CreateProductsFunction))]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "products")] HttpRequest req, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        if (req is null)
        {
            return new BadRequestObjectResult("Invalid request");
        }

        using var reader = new StreamReader(req.Body);
        var requestBody = await reader.ReadToEndAsync(cancellationToken);
        var createProductsRequest = JsonConvert.DeserializeObject<CreateProductsRequest>(requestBody);
        if (createProductsRequest is null)
        {
            return new BadRequestObjectResult("Invalid request");
        }

        var command = new CreateProductsCommand(createProductsRequest.Products);

        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure
            ? new BadRequestObjectResult(result.Error)
            : new OkObjectResult(result.Value);
    }
}
