using StileStream.Wms.Products.Application.Features.Products.ImportProducts.Contracts;
using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;

namespace StileStream.Wms.Products.Application.Features.Products.ImportProducts;
public sealed record ProductImportCommand(ProductImportRequest Request) : ICommand<Guid>;

