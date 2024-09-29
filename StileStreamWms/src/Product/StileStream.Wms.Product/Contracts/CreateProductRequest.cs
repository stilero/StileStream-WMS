namespace StileStream.Wms.Product.Contracts;

public sealed record CreateProductRequest(
    string Name,
    string Sku,
    string Description,
    string Manufacturer,
    string Category,
    string Status,
    string CreatedBy,
    string UpdatedBy);
