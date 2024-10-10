
using StileStream.Wms.Products.Application.Features.CreateProducts.Contracts;
using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;

namespace StileStream.Wms.Products.Application.Features.CreateProducts;

public sealed record CreateProductsCommand(IReadOnlyCollection<CreateProductRequest> Products) : ICommand<CreateProductsResponse>;
