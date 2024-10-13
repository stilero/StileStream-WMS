using StileStream.Wms.SharedKernel.Application.Models.Results;

namespace StileStream.Wms.Products.Application.Features.Products.ImportProducts.Errors;
public static class ImportProductsErrors
{
    public static ErrorResult InvalidRequest => ErrorResult.Validation("ImportProductsErrors.InvalidRequest", "Request cannot be null");
}
