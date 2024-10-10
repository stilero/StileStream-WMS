using StileStream.Wms.Products.Domain.Aggregates.ProductImports;
using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.Events;

public sealed class ProductImportCreatedEvent : DomainEvent
{
    public string ImportType { get; } = string.Empty;

    public ProductImportCreatedEvent(ProductImport productImport) : base(productImport.Id, nameof(ProductImport), nameof(ProductImportCreatedEvent))
    {
        ImportType = productImport.Type.ToString();
    }
}
