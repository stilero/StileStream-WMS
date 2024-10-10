using StileStream.Wms.Products.Domain.Aggregates.ProductImports;

namespace StileStream.Wms.Products.Application.Features.ImportProducts.Contracts;
public sealed record ProductImportRequest(ImportType ImportType, IReadOnlyCollection<ProductData> Data);
