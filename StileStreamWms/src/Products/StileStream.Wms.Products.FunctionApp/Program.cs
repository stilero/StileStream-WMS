using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StileStream.Wms.Products.Persistance.Common;

using StileStream.Wms.Products.Application;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.AddProductsApplication();
        services.AddProductsInfrastructure(configuration);
    })
    .Build();

host.Run();
