using MediatR;
using Shared.Domain.Models.Results;

namespace Shared.Domain.Interfaces.MediatR;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;
