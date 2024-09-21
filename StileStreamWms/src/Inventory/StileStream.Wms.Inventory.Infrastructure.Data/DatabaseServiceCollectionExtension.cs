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
        var connectionString = configuration.GetConnectionString("SqlServer") ?? configuration["ConnectionStrings__SqlServer"] ?? throw new ArgumentNullException("Connection string not found");
        services.AddDbContext<InventoryServiceDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, InventoryUnitOfWork>();
        services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
        return services;
    }
}
