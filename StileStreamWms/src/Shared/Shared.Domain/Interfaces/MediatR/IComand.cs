using MediatR;
using Shared.Domain.Models.Results;

namespace Shared.Domain.Interfaces.MediatR;

public interface ICommand : IRequest<Result>;
