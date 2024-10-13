using StileStream.Wms.Products.Application.Features.Products.CreateProducts.Contracts;
using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;

namespace StileStream.Wms.Products.Application.Features.Products.CreateProducts;

public sealed record CreateProductsCommand(IReadOnlyCollection<CreateProductRequest> Products) : ICommand<CreateProductsResponse>;
