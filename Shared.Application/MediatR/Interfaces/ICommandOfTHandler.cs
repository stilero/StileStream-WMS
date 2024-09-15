using MediatR;
using SharedKernel.Domain.Models.Results;

namespace SharedKernel.Application.MediatR.Interfaces;

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>;
