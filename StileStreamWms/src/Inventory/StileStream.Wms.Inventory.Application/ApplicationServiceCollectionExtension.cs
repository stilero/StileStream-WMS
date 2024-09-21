using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using StileStream.Wms.SharedKernel.Application.MediatR.PipelineBehaviors;

namespace StileStream.Wms.Inventory.Application;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceCollectionExtension).Assembly);
        services.AddMediatR(config =>
        {
            config.AddOpenBehavior(typeof(FluentValidationBehavior<,>));
            config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            config.RegisterServicesFromAssembly(typeof(ApplicationServiceCollectionExtension).Assembly);
        });

        return services;
    }
}
