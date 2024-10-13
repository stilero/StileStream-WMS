using StileStream.Wms.Products.Domain.ProductImport.ValueObjects;

namespace StileStream.Wms.Products.Application.Features.Products.ImportProducts.Contracts;
public sealed record ProductImportRequest(ImportType ImportType, IReadOnlyCollection<ProductData> Data);
