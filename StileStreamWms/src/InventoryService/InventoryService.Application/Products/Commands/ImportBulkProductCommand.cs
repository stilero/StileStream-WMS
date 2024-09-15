using System.Collections.ObjectModel;

using InventoryService.Application.Products.Requests;
using InventoryService.Application.Products.Responses;

using Shared.Application.MediatR.Interfaces;

namespace InventoryService.Application.Products.Commands;

public sealed record class ImportBulkProductCommand : ICommand<ImportBulkProductResponse>
{
    public ReadOnlyCollection<ImportProductRequest> Products { get; }

    public ImportBulkProductCommand(ReadOnlyCollection<ImportProductRequest> products)
    {
        Products = products;
    }
}
