using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using StileStream.Wms.Products.Application;
using StileStream.Wms.Products.FunctionApp.Functions;
using StileStream.Wms.Products.Infrastructure;

namespace StileStream.Wms.Products.Integration.Tests.Fixtures;
public class AzureFunctionFixture : SqlServerTestFixture, IAsyncLifetime
{
    public IHost? Host { get; protected set; }
    public HttpClient? HttpClient { get; protected set; }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        ConfigureFunctionHost();

        if (Host == null)
        {
            throw new InvalidOperationException("Host not initialized");
        }

        await Host.StartAsync();

        HttpClient = Host.GetTestClient();

        await ApplyMigrationsAsync();
    }

    private async Task ApplyMigrationsAsync()
    {
        using var scope = Host!.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    private void ConfigureFunctionHost() => Host = new HostBuilder()
            //.ConfigureAppConfiguration(SetEnvironmentVariables)
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseTestServer();
                builder.Configure(ConfigureApp);
                builder.ConfigureServices(ConfigureServices);
            })
            .Build();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "ASP0018:Unused route parameter", Justification = "Routing parameters not recognized by IDE")]
    private static void ConfigureApp(IApplicationBuilder app)
    {
        app.UseRouting();
        //app.UseExceptionHandler();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapPost("api/products/import", async context =>
            {
                var function = context.RequestServices.GetRequiredService<ProductImportFunction>();
                var actionResult = await function.RunAsync(context.Request, CancellationToken.None);
                switch (actionResult)
                {
                    case ObjectResult objectResult:
                        context.Response.StatusCode = objectResult.StatusCode ?? StatusCodes.Status200OK;
                        await context.Response.WriteAsJsonAsync(objectResult.Value);
                        break;
                    case StatusCodeResult statusCodeResult:
                        context.Response.StatusCode = statusCodeResult.StatusCode;
                        break;
                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }
            });

            endpoints.MapPost("api/products", async context =>
            {
                var function = context.RequestServices.GetRequiredService<CreateProductsFunction>();
                var actionResult = await function.Run(context.Request, CancellationToken.None);
                switch (actionResult)
                {
                    case ObjectResult objectResult:
                        context.Response.StatusCode = objectResult.StatusCode ?? StatusCodes.Status200OK;
                        await context.Response.WriteAsJsonAsync(objectResult.Value);
                        break;
                    case StatusCodeResult statusCodeResult:
                        context.Response.StatusCode = statusCodeResult.StatusCode;
                        break;
                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }
            });

            endpoints.MapDelete("api/products/{id:guid}", async context =>
            {
                if (context.Request.RouteValues.TryGetValue("id", out var productIdObj) && Guid.TryParse(productIdObj?.ToString(), out var id))
                {
                    var function = context.RequestServices.GetRequiredService<DeleteProductFunction>();

                    var actionResult = await function.Run(context.Request, id, CancellationToken.None);
                    switch (actionResult)
                    {
                        case ObjectResult objectResult:
                            context.Response.StatusCode = objectResult.StatusCode ?? StatusCodes.Status200OK;
                            await context.Response.WriteAsJsonAsync(objectResult.Value);
                            break;
                        case StatusCodeResult statusCodeResult:
                            context.Response.StatusCode = statusCodeResult.StatusCode;
                            break;
                        default:
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            break;
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Invalid or missing productId");
                }
            });

        });
    }

    private void ConfigureServices(IServiceCollection services)
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            ["ConnectionStrings:SqlServer"] = ConnectionString
        };
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        services.AddProductsInfrastructure(configuration);
        services.AddProductsApplication();
        services.AddScoped<CreateProductsFunction>();
        services.AddScoped<DeleteProductFunction>();
        services.AddScoped<ProductImportFunction>();
    }

    public override async Task DisposeAsync()
    {
        if (Host != null)
        {
            await Host.StopAsync();
        }

        HttpClient?.Dispose();
        await base.DisposeAsync();
    }
}
