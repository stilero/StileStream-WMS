using StileStream.Wms.SharedKernel.Domain.Primitives;

namespace StileStream.Wms.Products.Domain.ProductImport.Events;

public sealed class ProductImportValidationFailed : DomainEvent
{
    public ProductImportValidationFailed(ProductImport productImport) : base(productImport.Id, nameof(ProductImport), nameof(ProductImportValidated))
    {
    }
}
