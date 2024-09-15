using MediatR;
using Shared.Domain.Models.Results;

namespace Shared.Application.MediatR.Interfaces;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;
