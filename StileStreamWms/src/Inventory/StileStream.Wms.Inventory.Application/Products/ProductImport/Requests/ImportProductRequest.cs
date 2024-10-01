namespace StileStream.Wms.Inventory.Application.Products.ProductImport.Requests;
public sealed record ImportProductRequest(
    string Name,
    string Sku,
    string Description,
    string Manufacturer,
    string Category,
    string Status,
    string CreatedBy,
    string UpdatedBy);
