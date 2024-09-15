using MediatR;
using Shared.Domain.Models.Results;

namespace Shared.Domain.Interfaces.MediatR;

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>;
