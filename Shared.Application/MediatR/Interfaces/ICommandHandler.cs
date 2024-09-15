using MediatR;
using SharedKernel.Domain.Models.Results;

namespace SharedKernel.Application.MediatR.Interfaces;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;
