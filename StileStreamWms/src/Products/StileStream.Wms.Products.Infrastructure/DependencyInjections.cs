using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StileStream.Wms.Products.Domain.Repositories;
using StileStream.Wms.Products.Infrastructure.Repositories;
using StileStream.Wms.SharedKernel.Domain.Interfaces;
using StileStream.Wms.SharedKernel.Infrastructure.Data.Interceptors;

namespace StileStream.Wms.Products.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        var connectionString = configuration.GetConnectionString("SqlServer")
            ?? configuration["ConnectionStrings__SqlServer"]
            ?? throw new ArgumentException("Connection string not found");

        services.AddDbContext<ProductsDbContext>((sp, options) => {
            var domainEventsToOutboxInterceptor = sp.GetRequiredService<SaveDomainEventsToOutboxMessagesInterceptor>();
            options.UseSqlServer(connectionString)
                .AddInterceptors(domainEventsToOutboxInterceptor);
            });
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, ProductsUnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
