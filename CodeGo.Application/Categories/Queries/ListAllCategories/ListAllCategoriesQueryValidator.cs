
using FluentValidation;

namespace CodeGo.Application.Categories.Queries.ListAllCategories;

public class ListAllCategoriesQueryValidator : AbstractValidator<ListAllCategoriesQuery>
{
    public ListAllCategoriesQueryValidator()
    {
        RuleFor(x => x.Page)
            .NotEmpty();
        RuleFor(x => x.PageSize)
            .NotEmpty()
            .LessThanOrEqualTo(20);
    }
}
