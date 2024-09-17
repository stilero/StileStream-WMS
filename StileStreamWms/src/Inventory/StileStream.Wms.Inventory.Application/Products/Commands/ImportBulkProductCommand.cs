using System.Collections.ObjectModel;

using StileStream.Wms.SharedKernel.Application.MediatR.Interfaces;

using StileStream.Wms.Inventory.Application.Products.Requests;
using StileStream.Wms.Inventory.Application.Products.Responses;

namespace StileStream.Wms.Inventory.Application.Products.Commands;

public sealed record class ImportBulkProductCommand : ICommand<ImportBulkProductResponse>
{
    public ReadOnlyCollection<ImportProductRequest> Products { get; }

    public ImportBulkProductCommand(ReadOnlyCollection<ImportProductRequest> products)
    {
        Products = products;
    }
}
