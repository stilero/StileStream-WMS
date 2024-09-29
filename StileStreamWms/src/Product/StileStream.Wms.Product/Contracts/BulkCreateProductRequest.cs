namespace StileStream.Wms.Product.Contracts;

public sealed record BulkCreateProductRequest(IReadOnlyCollection<CreateProductRequest> Products);
