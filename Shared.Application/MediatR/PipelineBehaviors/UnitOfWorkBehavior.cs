using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Interfaces;

namespace SharedKernel.Application.MediatR.PipelineBehaviors;

public class UnitOfWorkBehavior<TRequest, TResponse, TDbContext>(IUnitOfWork<TDbContext> unitOfWork) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TDbContext : DbContext
{
    private readonly IUnitOfWork<TDbContext> _unitOfWork = unitOfWork;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var response = await next();
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return response;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
