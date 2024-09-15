using MediatR;
using SharedKernel.Domain.Models.Results;

namespace SharedKernel.Application.MediatR.Interfaces;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
