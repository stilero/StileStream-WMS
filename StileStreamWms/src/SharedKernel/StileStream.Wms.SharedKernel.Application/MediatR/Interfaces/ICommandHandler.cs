using MediatR;

using StileStream.Wms.SharedKernel.Application.Models.Results;

namespace StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;
