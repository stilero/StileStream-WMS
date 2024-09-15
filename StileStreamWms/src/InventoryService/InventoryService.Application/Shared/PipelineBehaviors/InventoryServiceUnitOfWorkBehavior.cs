using InventoryService.Infrastructure.Data;

using MediatR;

using SharedKernel.Application.MediatR.PipelineBehaviors;
using SharedKernel.Domain.Interfaces;

namespace InventoryService.Application.Shared.PipelineBehaviors;
public class InventoryServiceUnitOfWorkBehavior<TRequest, TResponse> : UnitOfWorkBehavior<TRequest, TResponse, InventoryServiceDbContext>
    where TRequest : IRequest<TResponse>
{
    public InventoryServiceUnitOfWorkBehavior(IUnitOfWork<InventoryServiceDbContext> unitOfWork) : base(unitOfWork)
    {
    }   
}
