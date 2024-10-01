using FluentValidation;

using StileStream.Wms.SharedKernel.Application.Validators;
using StileStream.Wms.Inventory.Application.Products.ProductImport.Errors;

namespace StileStream.Wms.Inventory.Application.Products.ProductImport.Requests;
public class ImportProductRequestValidator : AbstractValidator<ImportProductRequest>
{
    public ImportProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessageAndErrorCode(ProductImportError.NameIsRequired)
            .MaximumLength(100).WithMessageAndErrorCode(ProductImportError.NameExceedsMaxLength);

        RuleFor(x => x.Sku)
            .NotEmpty().WithMessageAndErrorCode(ProductImportError.SkuIsRequired)
            .MaximumLength(50).WithMessageAndErrorCode(ProductImportError.SkuExceedsMaxLength);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessageAndErrorCode(ProductImportError.DescriptionIsRequired)
            .MaximumLength(500).WithMessageAndErrorCode(ProductImportError.DescriptionExceedsMaxLength);

        RuleFor(x => x.Manufacturer)
            .NotEmpty().WithMessageAndErrorCode(ProductImportError.ManufacturerIsRequired)
            .MaximumLength(100).WithMessageAndErrorCode(ProductImportError.ManufacturerExceedsMaxLength);

        RuleFor(x => x.Category)
            .NotEmpty().WithMessageAndErrorCode(ProductImportError.CategoryIsRequired)
            .MaximumLength(100).WithMessageAndErrorCode(ProductImportError.CategoryExceedsMaxLength);

        RuleFor(x => x.Status)
            .NotEmpty().WithMessageAndErrorCode(ProductImportError.StatusIsRequired)
            .MaximumLength(50).WithMessageAndErrorCode(ProductImportError.StatusExceedsMaxLength);
    }
}
