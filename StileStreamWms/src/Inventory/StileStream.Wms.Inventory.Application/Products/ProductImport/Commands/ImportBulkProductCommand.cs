using System.Collections.ObjectModel;

using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;
using StileStream.Wms.Inventory.Application.Products.ProductImport.Requests;
using StileStream.Wms.Inventory.Application.Products.ProductImport.Responses;

namespace StileStream.Wms.Inventory.Application.Products.ProductImport.Commands;

public sealed record class ImportBulkProductCommand : ICommand<ImportBulkProductResponse>
{
    public ReadOnlyCollection<ImportProductRequest> Products { get; }

    public ImportBulkProductCommand(ReadOnlyCollection<ImportProductRequest> products)
    {
        Products = products;
    }
}
