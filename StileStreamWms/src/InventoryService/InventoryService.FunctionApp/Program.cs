using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using InventoryService.Application;
using StileStream.Wms.Inventory.Infrastructure;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.AddDatabase(configuration);
        services.AddApplication();
    })
    .Build();

host.Run();
