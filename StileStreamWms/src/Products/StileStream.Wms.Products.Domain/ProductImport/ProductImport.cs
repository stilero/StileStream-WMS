using StileStream.Wms.Products.Domain.ProductImport.Entities;
using StileStream.Wms.Products.Domain.ProductImport.Events;
using StileStream.Wms.Products.Domain.ProductImport.ValueObjects;
using StileStream.Wms.Products.Domain.Products;
using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.ProductImport;
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

    private void ValidateStagedData()
    {
        foreach (var stagedData in StagedDatas)
        {
            stagedData.Validate();
        }

        Status = StagedDatas.Any(x => x.Status == StagingStatus.Invalid) ? ImportStatus.Failed : ImportStatus.Completed;
        if (Status == ImportStatus.Completed)
        {
            RaiseDomainEvent(new ProductImportValidatedEvent(this));
        }else
        {
            RaiseDomainEvent(new ProductImportValidationFailedEvent(this));
        }
       
    }

    public IEnumerable<Product> ProcessImportAndReturnProducts()
    {
        ValidateStagedData();
        var products = new List<Product>();
        if (Type == ImportType.New)
        {

            foreach (var stagedData in StagedDatas)
            {
                if (stagedData.Status == StagingStatus.Validated)
                {
                    var product = Product.CreateNew(stagedData.ProductName, stagedData.ProductSku, stagedData.ProductDescription, stagedData.ProductManufacturer, stagedData.ProductCategory);
                    products.Add(product);
                }
            }
        }
        else
        {
            foreach (var stagedData in StagedDatas)
            {
                if (stagedData.Status == StagingStatus.Validated)
                {
                    var product = Product.Update(Guid.NewGuid(), stagedData.ProductName, stagedData.ProductSku, stagedData.ProductStatus, stagedData.ProductManufacturer, stagedData.ProductDescription, stagedData.ProductCategory);
                    products.Add(product);
                }
            }
        }
        return products;
    }
}
