using StileStream.Wms.Products.Domain.Aggregates;
using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.Events;
public sealed class ProductCreatedEvent : DomainEvent
{
    public Product Product { get; private set; }

    public ProductCreatedEvent(Product product)
        : base(product?.Id ?? throw new ArgumentNullException(nameof(product)), nameof(Product), nameof(ProductCreatedEvent))
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
    }
}

