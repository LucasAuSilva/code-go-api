
using FluentValidation;

namespace CodeGo.Application.Categories.Command.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(350);
        RuleFor(x => x.LanguageValue)
            .NotEmpty()
            .LessThanOrEqualTo(4);
    }
}
