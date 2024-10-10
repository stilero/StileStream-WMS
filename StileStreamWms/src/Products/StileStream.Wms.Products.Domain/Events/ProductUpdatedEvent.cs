using StileStream.Wms.Products.Domain.Aggregates;
using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.Events;
public sealed class ProductUpdatedEvent : DomainEvent
{
    public Product Product { get; private set; }

    public ProductUpdatedEvent(Product product)
        : base(product?.Id ?? throw new ArgumentNullException(nameof(product)), nameof(Product), nameof(ProductUpdatedEvent))
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
    }
}

