using FluentValidation;
using SharedKernel.Domain.Models.Results;

namespace SharedKernel.Application.Validators;

public static class ValidatorExtensions
{
    private static readonly ErrorResult UnknownError = ErrorResult.Failure("Error.Unknown", "An unknown error occurred");

    public static IRuleBuilderOptions<T, TProperty> WithMessageAndErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, ErrorResult errorResult) =>
        rule
            .WithMessage(errorResult?.Message ?? UnknownError.Message)
            .WithErrorCode(errorResult?.Code ?? UnknownError.Code);
}
