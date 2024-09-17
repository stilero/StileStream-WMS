using FluentValidation;

using StileStream.Wms.SharedKernel.Application.Validators;

using StileStream.Wms.Inventory.Application.Products.Errors;

namespace StileStream.Wms.Inventory.Application.Products.Requests;
public class ImportProductRequestValidator : AbstractValidator<ImportProductRequest>
{
    public ImportProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessageAndErrorCode(ProductError.NameIsRequired)
            .MaximumLength(100).WithMessageAndErrorCode(ProductError.NameExceedsMaxLength);

        RuleFor(x => x.Sku)
            .NotEmpty().WithMessageAndErrorCode(ProductError.SkuIsRequired)
            .MaximumLength(50).WithMessageAndErrorCode(ProductError.SkuExceedsMaxLength);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessageAndErrorCode(ProductError.DescriptionIsRequired)
            .MaximumLength(500).WithMessageAndErrorCode(ProductError.DescriptionExceedsMaxLength);

        RuleFor(x => x.Manufacturer)
            .NotEmpty().WithMessageAndErrorCode(ProductError.ManufacturerIsRequired)
            .MaximumLength(100).WithMessageAndErrorCode(ProductError.ManufacturerExceedsMaxLength);

        RuleFor(x => x.Category)
            .NotEmpty().WithMessageAndErrorCode(ProductError.CategoryIsRequired)
            .MaximumLength(100).WithMessageAndErrorCode(ProductError.CategoryExceedsMaxLength);

        RuleFor(x => x.Status)
            .NotEmpty().WithMessageAndErrorCode(ProductError.StatusIsRequired)
            .MaximumLength(50).WithMessageAndErrorCode(ProductError.StatusExceedsMaxLength);
    }
}
