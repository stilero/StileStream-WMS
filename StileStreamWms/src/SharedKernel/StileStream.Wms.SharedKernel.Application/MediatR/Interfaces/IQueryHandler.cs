using MediatR;

using StileStream.Wms.SharedKernel.Application.Models.Results;

namespace StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
