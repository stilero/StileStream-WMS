using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.ProductImport.Events;

public sealed class ProductImportValidatedEvent : DomainEvent
{
    public ProductImportValidatedEvent(ProductImport productImport) : base(productImport.Id, nameof(ProductImport), nameof(ProductImportValidatedEvent))
    {
    }
}
