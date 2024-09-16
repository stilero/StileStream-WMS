using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using SharedKernel.Application.MediatR.PipelineBehaviors;

namespace InventoryService.Application;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceCollectionExtension).Assembly);
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        services.AddMediatR(config =>
        {
            config.AddOpenBehavior(typeof(FluentValidationBehavior<,>));
            config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            config.RegisterServicesFromAssembly(typeof(ApplicationServiceCollectionExtension).Assembly);
        });
        

        return services;
    }
}
