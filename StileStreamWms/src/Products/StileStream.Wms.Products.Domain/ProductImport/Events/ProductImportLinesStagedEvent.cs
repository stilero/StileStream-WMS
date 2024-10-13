using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.ProductImport.Events;

public sealed class ProductImportLinesStagedEvent : DomainEvent
{
    public ProductImportLinesStagedEvent(ProductImport productImport) : base(productImport.Id, nameof(ProductImport), nameof(ProductImportLinesStagedEvent))
    {
    }
}
