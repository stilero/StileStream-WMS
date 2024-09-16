using InventoryService.Domain.Repositories;
using InventoryService.Infrastructure.Data;
using InventoryService.Infrastructure.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SharedKernel.Domain.Interfaces;

namespace InventoryService.Infrastructure;

public static class DatabaseServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer") ?? configuration["ConnectionStrings__SqlServer"] ?? throw new ArgumentNullException("Connection string not found");
        services.AddDbContext<InventoryServiceDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Values:ConnectionStrings:SqlServer")));
        services.AddScoped(typeof(IUnitOfWork), typeof(InventoryUnitOfWork));
        services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
        return services;
    }
}
