using StileStream.Wms.Products.Domain.Aggregates;
using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.Events;
public sealed class ProductDataValidatedEvent : DomainEvent
{
    public IReadOnlyCollection<Guid> ProductIds { get; private set; }

    public ProductDataValidatedEvent(Guid productImportId, IReadOnlyCollection<Guid> productIds) : base(productImportId, nameof(ProductImportAggregate), nameof(ProductDataValidatedEvent))
    {
        ProductIds = productIds;
    }
}
