using FluentValidation;

using StileStream.Wms.Inventory.Application.Products.Requests;

using StileStream.Wms.SharedKernel.Application.Validators;

using StileStream.Wms.Inventory.Application.Products.Errors;

namespace StileStream.Wms.Inventory.Application.Products.Commands;
public class ImportBulkProductCommandValidator : AbstractValidator<ImportBulkProductCommand>
{
    public ImportBulkProductCommandValidator()
    {
        RuleFor(x => x.Products)
            .NotEmpty().WithMessageAndErrorCode(ProductError.NoProductsToImport)
            .Must(x => x.Count > 0).WithMessageAndErrorCode(ProductError.NoProductsToImport);

        RuleForEach(x => x.Products)
            .SetValidator(new ImportProductRequestValidator());

    }
}
