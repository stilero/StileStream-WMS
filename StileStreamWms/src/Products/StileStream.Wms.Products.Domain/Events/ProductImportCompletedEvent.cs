using StileStream.Wms.Products.Domain.Aggregates;
using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.Events;
public sealed class ProductImportCompletedEvent : DomainEvent
{
    public ProductImportCompletedEvent(Guid productImportId) 
        : base(productImportId, nameof(ProductImportAggregate), nameof(ProductImportCompletedEvent))
    {
    }
}
