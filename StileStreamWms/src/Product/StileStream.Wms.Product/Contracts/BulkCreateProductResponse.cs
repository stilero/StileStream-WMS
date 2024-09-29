namespace StileStream.Wms.Product.Contracts;

public sealed record BulkCreateProductResponse(IReadOnlyCollection<Guid> ProductIds);
