namespace StileStream.Wms.Products.Application.Features.CreateProducts.Contracts;

public sealed record CreateProductsResponse(IReadOnlyCollection<Guid> ProductIds);
