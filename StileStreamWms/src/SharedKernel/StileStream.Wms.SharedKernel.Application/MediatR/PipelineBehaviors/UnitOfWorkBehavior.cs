using MediatR;

using StileStream.Wms.SharedKernel.Domain.Interfaces;

namespace StileStream.Wms.SharedKernel.Application.MediatR.PipelineBehaviors;

public class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork, IMediator mediator) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMediator _mediator = mediator;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next, nameof(next));
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var response = await next();
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            await DispatchDomainEvents(cancellationToken);
            return response;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    private async Task DispatchDomainEvents(CancellationToken cancellationToken)
    {
        var domainEntities = _unitOfWork.GetTrackedEntities();
        var domainEvents = domainEntities.SelectMany(x => x.GetDomainEvents()).ToList();
        domainEntities.ToList().ForEach(entity => entity.ClearDomainEvents());

        var tasks = domainEvents.Select(async domainEvent => await _mediator.Publish(domainEvent, cancellationToken));

        await Task.WhenAll(tasks);
    }
}
