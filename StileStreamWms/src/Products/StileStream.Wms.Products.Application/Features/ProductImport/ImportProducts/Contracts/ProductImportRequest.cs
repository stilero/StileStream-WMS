using StileStream.Wms.Products.Domain.ProductImport.ValueObjects;

namespace StileStream.Wms.Products.Application.Features.ProductImport.ImportProducts.Contracts;
public sealed record ProductImportRequest(ImportType ImportType, IReadOnlyCollection<ProductData> Data);
