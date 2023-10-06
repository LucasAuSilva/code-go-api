
using FluentValidation;

namespace CodeGo.Application.Users.Queries.ListUsersByName;

public class ListUsersByNameQueryValidator : AbstractValidator<ListUsersByNameQuery>
{
    public ListUsersByNameQueryValidator()
    {
        RuleFor(x => x.Page)
            .NotEmpty();
        RuleFor(x => x.PageSize)
            .NotEmpty()
            .LessThanOrEqualTo(20);
    }
}
