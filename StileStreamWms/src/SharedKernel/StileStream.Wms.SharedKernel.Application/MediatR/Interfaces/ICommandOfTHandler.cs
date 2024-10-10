using MediatR;

using StileStream.Wms.SharedKernel.Application.Models.Results;

namespace StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>;
