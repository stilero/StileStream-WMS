using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.ProductImport.Events;

public sealed class ProductImportValidationFailedEvent : DomainEvent
{
    public ProductImportValidationFailedEvent(ProductImport productImport) : base(productImport.Id, nameof(ProductImport), nameof(ProductImportValidatedEvent))
    {
    }
}
