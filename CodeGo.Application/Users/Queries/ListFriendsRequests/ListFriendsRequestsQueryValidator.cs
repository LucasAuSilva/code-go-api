
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Users.Queries.ListFriendsRequests;

public class ListFriendsRequestsQueryValidator : AbstractValidator<ListFriendsRequestsQuery>
{
    public ListFriendsRequestsQueryValidator()
    {
        RuleFor(x => x.LoggedUserId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.UserId)
            .NotEmpty()
            .IsId()
            .Equal(x => x.LoggedUserId);
        RuleFor(x => x.Status)
            .NotEmpty()
            .LessThan(6);
    }
}
