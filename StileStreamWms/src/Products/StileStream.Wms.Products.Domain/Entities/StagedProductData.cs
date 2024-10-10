namespace StileStream.Wms.Products.Domain.Entities;
public sealed class StagedProductData
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public string SKU { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Manufacturer { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public ValidationStatus ValidationStatus { get; private set; } = ValidationStatus.Pending;
    public string ValidationErrors { get; private set; } = string.Empty;

    public StagedProductData(string productName, string sku, string description, string manufacturer, string category)
    {

        ProductId = Guid.NewGuid();
        ProductName = productName;
        SKU = sku;
        Manufacturer = manufacturer;
        Description = description;
        Category = category;
        ValidationStatus = ValidationStatus.Pending;   
    }

    public ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(ProductName))
        {
            MarkAsInvalid($"{ProductId}: Product Name is required");
            return new ValidationResult(false, "Product Name is required");
        }

        if (string.IsNullOrWhiteSpace(SKU))
        {
            MarkAsInvalid($"{ProductId}: SKU is required");
            return new ValidationResult(false, "SKU is required");
        }

        if (string.IsNullOrWhiteSpace(Manufacturer))
        {
            MarkAsInvalid($"{ProductId}: Manufacturer is required");
            return new ValidationResult(false, "Manufacturer is required");
        }

        if (string.IsNullOrWhiteSpace(Category))
        {
            MarkAsInvalid($"{ProductId}: Category is required");
            return new ValidationResult(false, "Category is required");
        }

        return new ValidationResult(true, string.Empty);
    }

    public void MarkAsValidated() => ValidationStatus = ValidationStatus.Validated;

    public void MarkAsInvalid(string validationErrors)
    {
        ValidationStatus = ValidationStatus.Invalid;
        ValidationErrors = validationErrors;
    }
}


public sealed class ValidationResult
{
    public bool IsValid { get; }
    public bool IsInvalid => !IsValid;
    public string? ErrorMessage { get; }

    public static ValidationResult Success() => new(true);
    public static ValidationResult Fail(string errorMessage) => new(false, errorMessage);

    public ValidationResult(bool isValid, string? errorMessage = null)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }
}
public enum ValidationStatus
{
    Pending,
    Validated,
    Invalid
}
