using MassTransit;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Interfaces;
using StileStream.Wms.Products.Application.Features.Products.Repositories;
using StileStream.Wms.Products.Persistance.Common;
using StileStream.Wms.Products.Persistance.Common.Repositories;
using StileStream.Wms.Products.Persistance.Features.ProductImports.Repositories;
using StileStream.Wms.Products.Persistance.Features.Products;
using StileStream.Wms.SharedKernel.Domain.Interfaces;
using StileStream.Wms.SharedKernel.Infrastructure.Data.Interceptors;

namespace StileStream.Wms.Products.Persistance.Common;

public static class DependencyInjections
{

    public static IServiceCollection AddProductsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services.AddDatabase(configuration);
        services.AddRepositories();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        var connectionString = configuration.GetConnectionString("SqlServer")
            ?? configuration["ConnectionStrings__SqlServer"]
            ?? throw new ArgumentException("Connection string not found");

        services.AddSingleton<SaveDomainEventsToOutboxMessagesInterceptor>();
        services.AddSingleton<AuditSaveChangesInterceptor>();

        services.AddDbContext<ProductsDbContext>((sp, options) =>
        {
            var domainEventsToOutboxInterceptor = sp.GetRequiredService<SaveDomainEventsToOutboxMessagesInterceptor>();
            var auditSaveChangesInterceptor = sp.GetRequiredService<AuditSaveChangesInterceptor>();
            options.UseSqlServer(connectionString)
                .AddInterceptors(domainEventsToOutboxInterceptor, auditSaveChangesInterceptor);
        });
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, ProductsUnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductImportRepository, ProductImportRepository>();
        services.AddScoped<IProductImportLineRepository, ProductImportLineRepository>();
        return services;
    }

    public static IServiceCollection AddMassTransit(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddEntityFrameworkOutbox<ProductsDbContext>(o =>
            {
                o.UseSqlServer().UseBusOutbox();
                o.QueryDelay = TimeSpan.FromMilliseconds(100);
            });
        });

        return services;
    }
}
