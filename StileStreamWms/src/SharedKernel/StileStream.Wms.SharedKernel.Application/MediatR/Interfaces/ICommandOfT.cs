using MediatR;

using StileStream.Wms.SharedKernel.Domain.Models.Results;

namespace StileStream.Wms.SharedKernel.Application.MediatR.Interfaces
{
    public interface ICommand<T> : IRequest<Result<T>>;
}
