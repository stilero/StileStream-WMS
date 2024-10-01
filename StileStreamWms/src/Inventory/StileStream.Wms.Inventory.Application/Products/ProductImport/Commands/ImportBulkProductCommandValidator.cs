using FluentValidation;

using StileStream.Wms.Inventory.Application.Products.ProductImport.Errors;
using StileStream.Wms.Inventory.Application.Products.ProductImport.Requests;
using StileStream.Wms.SharedKernel.Application.Validators;

namespace StileStream.Wms.Inventory.Application.Products.ProductImport.Commands;
public class ImportBulkProductCommandValidator : AbstractValidator<ImportBulkProductCommand>
{
    public ImportBulkProductCommandValidator()
    {
        RuleFor(x => x.Products)
            .NotEmpty().WithMessageAndErrorCode(ProductImportError.NoProductsToImport)
            .Must(x => x.Count > 0).WithMessageAndErrorCode(ProductImportError.NoProductsToImport);

        RuleForEach(x => x.Products)
            .SetValidator(new ImportProductRequestValidator());

    }
}
