using StileStream.Wms.Products.Domain.Aggregates.ProductImports;
using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.Events;

public sealed class ProductImportStagedEvent : DomainEvent
{
    public ProductImportStagedEvent(ProductImport productImport) : base(productImport.Id, nameof(ProductImport), nameof(ProductImportStagedEvent))
    {
    }
}
