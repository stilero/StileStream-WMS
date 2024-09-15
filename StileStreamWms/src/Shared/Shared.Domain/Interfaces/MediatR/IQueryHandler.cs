using MediatR;
using Shared.Domain.Models.Results;

namespace Shared.Domain.Interfaces.MediatR;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
