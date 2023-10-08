
using FluentValidation;

namespace CodeGo.Application.Categories.Queries.ListAllCategories;

public class ListAllCategoriesQueryValidator : AbstractValidator<ListAllCategoriesQuery>
{
    public ListAllCategoriesQueryValidator()
    {
        RuleFor(x => x.Language)
            .NotEmpty();
    }
}
