using InventoryService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Infrastructure;

public static class DatabaseServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InventoryServiceDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Values:ConnectionStrings:SqlServer")));

        return services;
    }
}
