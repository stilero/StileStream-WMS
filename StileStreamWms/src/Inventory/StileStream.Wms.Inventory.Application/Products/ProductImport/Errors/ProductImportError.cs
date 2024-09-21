using StileStream.Wms.SharedKernel.Domain.Models.Results;

namespace StileStream.Wms.Inventory.Application.Products.ProductImport.Errors;
public static class ProductImportError
{
    public static ErrorResult NoProductsToImport => ErrorResult.Validation("ProductError.NoProductsToImport", "No products to import");
    public static ErrorResult InvalidRequest => ErrorResult.Validation("ProductError.InvalidRequest", "Invalid Request");
    public static ErrorResult NameIsRequired => ErrorResult.Validation("ProductError.NameIsRequired", "Name is required");
    public static ErrorResult NameExceedsMaxLength => ErrorResult.Validation("ProductError.NameExceedsMaxLength", "Name must not exceed 100 characters");
    public static ErrorResult SkuIsRequired => ErrorResult.Validation("ProductError.SkuIsRequired", "Sku is required");
    public static ErrorResult SkuExceedsMaxLength => ErrorResult.Validation("ProductError.SkuExceedsMaxLength", "Sku must not exceed 50 characters");
    public static ErrorResult DescriptionIsRequired => ErrorResult.Validation("ProductError.DescriptionIsRequired", "Description is required");
    public static ErrorResult DescriptionExceedsMaxLength => ErrorResult.Validation("ProductError.DescriptionExceedsMaxLength", "Description must not exceed 500 characters");
    public static ErrorResult ManufacturerIsRequired => ErrorResult.Validation("ProductError.ManufacturerIsRequired", "Manufacturer is required");
    public static ErrorResult ManufacturerExceedsMaxLength => ErrorResult.Validation("ProductError.ManufacturerExceedsMaxLength", "Manufacturer must not exceed 100 characters");
    public static ErrorResult CategoryIsRequired => ErrorResult.Validation("ProductError.CategoryIsRequired", "Category is required");
    public static ErrorResult CategoryExceedsMaxLength => ErrorResult.Validation("ProductError.CategoryExceedsMaxLength", "Category must not exceed 100 characters");
    public static ErrorResult StatusIsRequired => ErrorResult.Validation("ProductError.StatusIsRequired", "Status is required");
    public static ErrorResult StatusExceedsMaxLength => ErrorResult.Validation("ProductError.StatusExceedsMaxLength", "Status must not exceed 50 characters");
}
