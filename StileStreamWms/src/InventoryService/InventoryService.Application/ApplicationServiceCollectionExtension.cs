using FluentValidation;

using InventoryService.Infrastructure.Data.Repositories;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using SharedKernel.Application.MediatR.PipelineBehaviors;
using SharedKernel.Domain.Interfaces;

namespace InventoryService.Application;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceCollectionExtension).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        services.AddMediatR(config =>
        {
            config.AddOpenBehavior(typeof(FluentValidationBehavior<,>));
            //config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            config.RegisterServicesFromAssembly(typeof(ApplicationServiceCollectionExtension).Assembly);
        });
        

        return services;
    }
}
