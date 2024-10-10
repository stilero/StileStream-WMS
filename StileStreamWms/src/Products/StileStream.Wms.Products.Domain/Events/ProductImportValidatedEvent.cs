using StileStream.Wms.Products.Domain.Aggregates.ProductImports;
using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.Events;

public sealed class ProductImportValidatedEvent : DomainEvent
{
    public ProductImportValidatedEvent(ProductImport productImport) : base(productImport.Id, nameof(ProductImport), nameof(ProductImportValidatedEvent))
    {
    }
}
