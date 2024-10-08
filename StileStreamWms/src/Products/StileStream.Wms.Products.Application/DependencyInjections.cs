using System.Reflection;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using StileStream.Wms.SharedKernel.Application.MediatR.PipelineBehaviors;

namespace StileStream.Wms.Products.Application;
public static class DependencyInjections
{
    public static IServiceCollection AddProductsApplication(this IServiceCollection services)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(thisAssembly);
        services.AddMediatR(config =>
        {

            config.AddOpenBehavior(typeof(FluentValidationBehavior<,>));
            config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            config.RegisterServicesFromAssembly(thisAssembly);
        });    

        return services;
    }
}
