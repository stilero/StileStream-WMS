using MediatR;

using StileStream.Wms.SharedKernel.Application.Models.Results;

namespace StileStream.Wms.SharedKernel.Application.MediatR.Interfaces
{
    public interface ICommand<T> : IRequest<Result<T>>;
}
