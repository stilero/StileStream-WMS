using StileStream.Wms.Products.Application.Features.ProductImport.ImportProducts.Contracts;
using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;

namespace StileStream.Wms.Products.Application.Features.ProductImport.ImportProducts.Commands;
public sealed record ImportProductsCommand(ProductImportRequest Request) : ICommand<Guid>;

