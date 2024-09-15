using MediatR;
using SharedKernel.Domain.Models.Results;

namespace SharedKernel.Application.MediatR.Interfaces
{
    public interface ICommand<T> : IRequest<Result<T>>;
}
