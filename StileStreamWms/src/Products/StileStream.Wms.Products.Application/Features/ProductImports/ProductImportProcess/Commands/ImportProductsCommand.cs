using StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Contracts;
using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;

namespace StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Commands;
public sealed record ImportProductsCommand(ProductImportRequest Request) : ICommand<Guid>;

