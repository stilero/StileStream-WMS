using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using InventoryService.Infrastructure;
using Microsoft.Extensions.Configuration;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.AddDatabase(configuration);
    })
    .Build();

host.Run();
