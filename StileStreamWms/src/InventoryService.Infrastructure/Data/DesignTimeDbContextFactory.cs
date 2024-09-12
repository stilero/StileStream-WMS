using InventoryService.FunctionApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;


namespace InventoryService.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<InventoryServiceDbContext>
{
    public InventoryServiceDbContext CreateDbContext(string[] args)
    {
        // Get the path to an assembly within the Azure Functions project
        var azureFunctionAssembly = Assembly.GetAssembly(typeof(Function1)); // Use a known type from your Azure Functions project
        var assemblyPath = Path.GetDirectoryName(azureFunctionAssembly.Location);

        // Navigate up to the root of the Azure Functions project if necessary
        // Adjust if needed based on your directory structure
        var projectRootPath = Path.GetFullPath(Path.Combine(assemblyPath, @"..\..\..\..")); // Adjust this to navigate to the project root
        var configPath = Path.Combine(projectRootPath, "InventoryService.FunctionApp", "local.settings.json");

        var configuration = new ConfigurationBuilder()
                .SetBasePath(projectRootPath)
                .AddJsonFile(configPath, optional: false, reloadOnChange: true) // Adjust the path as necessary
                .AddEnvironmentVariables()
                .Build();

        var optionsBuilder = new DbContextOptionsBuilder<InventoryServiceDbContext>();
        var connectionString = configuration["Values:ConnectionStrings:DefaultConnection"];
        optionsBuilder.UseSqlServer(connectionString);
        return new InventoryServiceDbContext(optionsBuilder.Options);
    }
}
