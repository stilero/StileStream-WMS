using FluentValidation;

using InventoryService.Application.Products.Errors;
using Shared.Application.Validators;

namespace InventoryService.Application.Products.Commands;
public class ImportBulkProductCommandValidator : AbstractValidator<ImportBulkProductCommand>
{
    public ImportBulkProductCommandValidator()
    {
        RuleFor(x => x.Products)
            .NotEmpty().WithMessageAndErrorCode(ProductError.NoProductsToImport)
            .Must(x => x.Count > 0).WithMessageAndErrorCode(ProductError.NoProductsToImport);

        RuleForEach(x => x.Products)
            
    }
}
