using FluentValidation;
using MediatR;

namespace SharedKernel.Application.MediatR.PipelineBehaviors;

public class FluentValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await Task.WhenAll(_validators.Select(v => v.ValidateAndThrowAsync(request, cancellationToken)));

        return await next();
    }
}
