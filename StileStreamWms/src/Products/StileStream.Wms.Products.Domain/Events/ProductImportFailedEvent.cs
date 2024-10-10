using StileStream.Wms.Products.Domain.Aggregates;
using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.Events;
public sealed class ProductImportFailedEvent : DomainEvent
{
    public string ErrorMessage { get; private set; } = string.Empty;

    public ProductImportFailedEvent(Guid productImportId, string errorMessage) 
        : base(productImportId, nameof(ProductImportAggregate), nameof(ProductImportFailedEvent))
    {
        ErrorMessage = errorMessage;
    }
}
