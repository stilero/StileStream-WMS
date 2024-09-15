using FluentValidation;

using InventoryService.Infrastructure.Data;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using SharedKernel.Application.MediatR.PipelineBehaviors;

namespace InventoryService.Application;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceCollectionExtension).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,,>).MakeGenericType(
            typeof(IRequest<>),
            typeof(object),
            typeof(InventoryServiceDbContext)));
        services.AddMediatR(config =>
        {
            config.AddOpenBehavior(typeof(FluentValidationBehavior<,>));
            config.RegisterServicesFromAssembly(typeof(ApplicationServiceCollectionExtension).Assembly);
        });

        return services;
    }
}
