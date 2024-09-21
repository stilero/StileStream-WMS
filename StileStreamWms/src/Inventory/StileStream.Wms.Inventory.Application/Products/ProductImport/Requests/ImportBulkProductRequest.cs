namespace StileStream.Wms.Inventory.Application.Products.ProductImport.Requests;
public sealed record ImportBulkProductRequest(IReadOnlyCollection<ImportProductRequest> Products);
