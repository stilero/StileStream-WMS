using MediatR;
using SharedKernel.Domain.Models.Results;

namespace SharedKernel.Application.MediatR.Interfaces;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;