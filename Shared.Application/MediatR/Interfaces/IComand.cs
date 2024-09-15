using MediatR;
using SharedKernel.Domain.Models.Results;

namespace SharedKernel.Application.MediatR.Interfaces;

public interface ICommand : IRequest<Result>;
