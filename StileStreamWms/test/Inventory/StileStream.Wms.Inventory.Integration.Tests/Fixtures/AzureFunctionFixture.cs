using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using StileStream.Wms.Inventory.Application;
using StileStream.Wms.Inventory.FunctionApp.Products;
using StileStream.Wms.Inventory.Infrastructure.Data;

namespace StileStream.Wms.Inventory.Integration.Tests.Fixtures;
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
        var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
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

    private static void ConfigureApp(IApplicationBuilder app)
    {
        app.UseRouting();
        //app.UseExceptionHandler();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapPost("api/products/import", async context =>
            {
                var function = context.RequestServices.GetRequiredService<ImportBulkProduct>();
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
            
        });
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<InventoryDbContext>(options =>
            options.UseSqlServer(ConnectionString));
        services.AddRepositories();
        services.AddApplication();
        services.AddScoped<ImportBulkProduct>();
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
