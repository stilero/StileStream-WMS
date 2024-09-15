using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.MediatR.PipelineBehaviors;

namespace InventoryService.Application;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceCollectionExtension).Assembly);
        services.AddMediatR(config =>
        {
            config.AddOpenBehavior(typeof(FluentValidationBehavior<,>));
            config.RegisterServicesFromAssembly(typeof(ApplicationServiceCollectionExtension).Assembly);
        });

        return services;
    }
}