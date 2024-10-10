using StileStream.Wms.Products.Application.Features.ImportProducts.Contracts;
using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;

namespace StileStream.Wms.Products.Application.Features.ImportProducts;
public sealed record ProductImportCommand(ProductImportRequest Request) : ICommand<Guid>;

