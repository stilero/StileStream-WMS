namespace StileStream.Wms.Products.Application.Features.Products.CreateProducts.Contracts;
public sealed record CreateProductsRequest(IReadOnlyCollection<CreateProductRequest> Products);

