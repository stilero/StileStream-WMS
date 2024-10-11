using StileStream.Wms.Products.Domain.Products;
using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.Products.Events;
public sealed class ProductDeletedEvent : DomainEvent
{
    public ProductDeletedEvent(Guid aggregateId) : base(aggregateId, nameof(Product), nameof(ProductDeletedEvent)) { }
}
