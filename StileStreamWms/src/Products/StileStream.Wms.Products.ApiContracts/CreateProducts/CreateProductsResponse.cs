namespace StileStream.Wms.Products.ApiContracts.CreateProducts;

public sealed record CreateProductsResponse(IReadOnlyCollection<Guid> ProductIds);
