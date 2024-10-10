using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.Aggregates;
public sealed class ProductImport : AggregateRoot
{
    public Guid Id { get; private set; }
    public ImportType Type { get; private set; } = ImportType.New;
    public ImportStatus Status { get; private set; } = ImportStatus.Pending;
    public ICollection<StagedProductData> StagedDatas { get; private set; } = [];

    public static ProductImport CreateNew(ImportType type)
    {
        var productImport = new ProductImport
        {
            Id = Guid.NewGuid(),
            Type = type
        };

        productImport.RaiseDomainEvent(new ProductImportCreatedEvent(productImport));

        return productImport;
    }

    public void StageData(ICollection<StagedProductData> stagedDatas)
    {
        StagedDatas = stagedDatas;
        Status = ImportStatus.Processing;
        RaiseDomainEvent(new ProductImportStagedEvent(this));
    }

    public void ValidateStagedData()
    {
        foreach (var stagedData in StagedDatas)
        {
            stagedData.Validate();
        }

        Status = StagedDatas.Any(x => x.Status == StagingStatus.Invalid) ? ImportStatus.Failed : ImportStatus.Completed;
        RaiseDomainEvent(new ProductImportValidatedEvent(this));
    }

}
internal class ProductImportCreatedEvent : DomainEvent
{
    public string ImportType { get; } = string.Empty;

    public ProductImportCreatedEvent(ProductImport productImport) : base(productImport.Id, nameof(ProductImport), nameof(ProductImportCreatedEvent))
    {
        ImportType = productImport.Type.ToString();
    }
}

public enum ImportStatus
{
    Pending,
    Processing,
    Completed,
    Failed,
}

public enum ImportType
{
    New,
    Update,
}

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
        => new (productName, productSku, productDescription, productManufacturer, productCategory, productStatus);

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

public enum StagingStatus
{
    Pending,
    Validated,
    Invalid,
}

internal class ProductImportStagedEvent : DomainEvent
{
    public ProductImportStagedEvent(ProductImport productImport) : base(productImport.Id, nameof(ProductImport), nameof(ProductImportStagedEvent))
    {
    }
}


internal class ProductImportValidatedEvent : DomainEvent
{
    public ProductImportValidatedEvent(ProductImport productImport) : base(productImport.Id, nameof(ProductImport), nameof(ProductImportValidatedEvent))
    {
    }
}
