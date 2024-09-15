using MediatR;
using Shared.Domain.Models.Results;

namespace Shared.Application.MediatR.Interfaces;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;