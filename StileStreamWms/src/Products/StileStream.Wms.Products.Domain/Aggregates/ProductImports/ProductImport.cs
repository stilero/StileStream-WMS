using StileStream.Wms.Products.Domain.Events;
using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.Aggregates.ProductImports;
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
        RaiseDomainEvent(new ProductImportValidatedEvent(this));
    }

    public void ProcessStagedData()
    {
        ValidateStagedData();
        if (Type == ImportType.New)
        {

            foreach (var stagedData in StagedDatas)
            {
                if (stagedData.Status == StagingStatus.Validated)
                {
                    var product = Product.CreateNew(stagedData.ProductName, stagedData.ProductSku, stagedData.ProductDescription, stagedData.ProductManufacturer, stagedData.ProductCategory);
                    RaiseDomainEvent(new ProductCreatedEvent(product));
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
                    RaiseDomainEvent(new ProductUpdatedEvent(product));
                }
            }
        }
    }
}
