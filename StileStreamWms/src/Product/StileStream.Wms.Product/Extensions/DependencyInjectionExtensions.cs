using StileStream.Wms.Product.Database;
using Microsoft.EntityFrameworkCore;
using StileStream.Wms.SharedKernel.Domain.Interfaces;
using StileStream.Wms.SharedKernel.Infrastructure.Data.Interceptors;
using StileStream.Wms.Product.Interfaces;
using StileStream.Wms.Product.Database.Products.Repositories;
using StileStream.Wms.SharedKernel.Infrastructure.Data.Repositories;
using StileStream.Wms.SharedKernel.Application.MediatR.PipelineBehaviors;
using System.Reflection;
using FluentValidation;

namespace StileStream.Wms.Product.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        var connectionString = configuration.GetConnectionString("SqlServer")
            ?? configuration["ConnectionStrings__SqlServer"]
            ?? throw new ArgumentException("Connection string not found");

        services.AddSingleton<SaveDomainEventsToOutboxMessagesInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) => {
            var domainEventsToOutboxInterceptor = sp.GetRequiredService<SaveDomainEventsToOutboxMessagesInterceptor>();
            options.UseSqlServer(connectionString)
                .AddInterceptors(domainEventsToOutboxInterceptor);
        });
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(config =>
        {
            config.AddOpenBehavior(typeof(FluentValidationBehavior<,>));
            config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}
