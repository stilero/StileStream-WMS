using StileStream.Wms.Products.Domain.ProductImport.ValueObjects;

namespace StileStream.Wms.Products.Domain.ProductImport.Entities;

public sealed class StagedProductData
{
    public Guid Id { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public string ProductSku { get; private set; } = string.Empty;
    public string ProductDescription { get; private set; } = string.Empty;
    public string ProductManufacturer { get; private set; } = string.Empty;
    public string ProductCategory { get; private set; } = string.Empty;
    public string ProductStatus { get; private set; } = string.Empty;
    public StagingStatus Status { get; private set; } = StagingStatus.Pending;
    public string Message { get; private set; } = string.Empty;

    public StagedProductData(string productName, string productSku, string productDescription, string productManufacturer, string productCategory, string productStatus)
    {
        ProductName = productName;
        ProductSku = productSku;
        ProductDescription = productDescription;
        ProductManufacturer = productManufacturer;
        ProductCategory = productCategory;
        ProductStatus = productStatus;
    }

    public static StagedProductData CreateNew(string productName, string productSku, string productDescription, string productManufacturer, string productCategory, string productStatus)
        => new(productName, productSku, productDescription, productManufacturer, productCategory, productStatus);

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(ProductName))
        {
            Status = StagingStatus.Invalid;
            Message = "Product name is required.";
        }
        else if (string.IsNullOrWhiteSpace(ProductSku))
        {
            Status = StagingStatus.Invalid;
            Message = "Product SKU is required.";
        }
        else if (string.IsNullOrWhiteSpace(ProductDescription))
        {
            Status = StagingStatus.Invalid;
            Message = "Product description is required.";
        }
        else if (string.IsNullOrWhiteSpace(ProductManufacturer))
        {
            Status = StagingStatus.Invalid;
            Message = "Product manufacturer is required.";
        }
        else if (string.IsNullOrWhiteSpace(ProductCategory))
        {
            Status = StagingStatus.Invalid;
            Message = "Product category is required.";
        }
        else if (string.IsNullOrWhiteSpace(ProductStatus))
        {
            Status = StagingStatus.Invalid;
            Message = "Product status is required.";
        }
        else
        {
            Status = StagingStatus.Validated;
        }
    }
}
