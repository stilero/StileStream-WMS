using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;

namespace StileStream.Wms.Products.Application.Features.Products.DeleteProduct;
public sealed record DeleteProductCommand(Guid Id) : ICommand;
