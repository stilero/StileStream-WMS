using StileStream.Wms.Products.Domain.Entities;
using StileStream.Wms.Products.Domain.Events;
using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.Aggregates;
public class ProductImportAggregate : AggregateRoot
{
    public Guid ImportId { get; private set; }
    public ImportStatus Status { get; private set; } = ImportStatus.Pending;
    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; private set; }
    public IList<StagedProductData> StagedProductData { get; private set; } = [];

    private ProductImportAggregate() { }

    public static ProductImportAggregate CreateNew()
    {
        var productImportAggregate = new ProductImportAggregate
        {
            ImportId = Guid.NewGuid(),
            Status = ImportStatus.Pending,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = null,
            StagedProductData = []
        };
        return productImportAggregate;
    }

    public void StageProductData(IEnumerable<StagedProductData> stagedProductData)
    {
        stagedProductData.ToList().ForEach(StagedProductData.Add);
        Status = ImportStatus.Staged;
        RaiseDomainEvent(new ProductImportStagedEvent(ImportId, stagedProductData.Select(x => x.ProductId).ToList().AsReadOnly()));
    }

    public void ValidateStagedData()
    {
        foreach (var stagedProduct in StagedProductData)
        {
            var validationResult = stagedProduct.Validate();

            if (validationResult.IsInvalid)
            {
                Status = ImportStatus.Failed;
                RaiseDomainEvent(new ProductImportFailedEvent(ImportId, validationResult.ErrorMessage!));
                return;
            }
            else
            {
                stagedProduct.MarkAsValidated();
            }
        }

        Status = ImportStatus.Validated;
        RaiseDomainEvent(new ProductDataValidatedEvent(ImportId, StagedProductData.Select(x => x.ProductId).ToList().AsReadOnly()));
    }

    public void CompleteImport()
    {
        Status = ImportStatus.Completed;
        RaiseDomainEvent(new ProductImportCompletedEvent(ImportId));
    }
}

public enum ImportStatus
{
    Pending,
    Staged,
    Processing,
    Validated,
    Completed,
    Failed
}
