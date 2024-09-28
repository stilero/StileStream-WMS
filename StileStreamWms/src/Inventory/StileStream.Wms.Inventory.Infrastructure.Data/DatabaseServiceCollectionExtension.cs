using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StileStream.Wms.SharedKernel.Domain.Interfaces;
using StileStream.Wms.Inventory.Infrastructure.Data.Repositories;
using StileStream.Wms.Inventory.Domain.Products.Repositories;
using StileStream.Wms.Inventory.Infrastructure.Data.Products.Repositories;

namespace StileStream.Wms.Inventory.Infrastructure.Data;

public static class DatabaseServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        var connectionString = configuration.GetConnectionString("SqlServer") ?? throw new ArgumentException("Connection string not found");
        services.AddDbContext<InventoryDbContext>(options => options.UseSqlServer(connectionString));
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, InventoryUnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
