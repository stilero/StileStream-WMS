using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using StileStream.Wms.Inventory.Application;
using StileStream.Wms.Inventory.Infrastructure.Data;
using Microsoft.Extensions.Logging;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddApplication();
    })
    .ConfigureLogging(logging => logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning))
    .Build();

host.Run();
