namespace StileStream.Wms.Products.Application.Features.CreateProducts.Contracts;
public sealed record CreateProductsRequest(IReadOnlyCollection<CreateProductRequest> Products);

