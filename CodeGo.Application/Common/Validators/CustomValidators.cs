
using FluentValidation;

namespace CodeGo.Application.Common.Validators;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, string> IsId<T>(
        this IRuleBuilder<T, string> ruleBuilder
    ) {
        return ruleBuilder.Must(x => Guid.TryParse(x, out var id))
            .WithMessage("This value must be an Guid");
    }
}
