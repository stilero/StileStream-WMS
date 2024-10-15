using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.ProductImport.Events;

public sealed class ProductImportStaged : DomainEvent
{
    public ProductImportStaged(ProductImport productImport) : base(productImport.Id, nameof(ProductImport), nameof(ProductImportStaged))
    {
    }
}
