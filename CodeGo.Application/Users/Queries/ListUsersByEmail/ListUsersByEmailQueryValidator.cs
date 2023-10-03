
using FluentValidation;

namespace CodeGo.Application.Users.Queries.ListUsersByEmail;

public class ListUsersByEmailQueryValidator : AbstractValidator<ListUsersByEmailQuery>
{
    public ListUsersByEmailQueryValidator()
    {
        RuleFor(x => x.Page)
            .NotEmpty();
        RuleFor(x => x.PageSize)
            .NotEmpty()
            .LessThanOrEqualTo(20);
    }
}
