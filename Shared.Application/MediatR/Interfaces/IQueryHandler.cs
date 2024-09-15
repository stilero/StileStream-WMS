using MediatR;
using Shared.Domain.Models.Results;

namespace Shared.Application.MediatR.Interfaces;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
