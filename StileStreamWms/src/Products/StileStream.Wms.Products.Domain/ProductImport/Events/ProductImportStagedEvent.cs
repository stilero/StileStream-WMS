using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.ProductImport.Events;

public sealed class ProductImportStagedEvent : DomainEvent
{
    public ProductImportStagedEvent(ProductImport productImport) : base(productImport.Id, nameof(ProductImport), nameof(ProductImportStagedEvent))
    {
    }
}
