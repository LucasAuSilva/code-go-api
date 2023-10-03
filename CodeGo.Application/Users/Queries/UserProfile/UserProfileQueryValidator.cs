
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Users.Queries.UserProfile;

public class UserProfileQueryValidator : AbstractValidator<UserProfileQuery>
{
    public UserProfileQueryValidator()
    {
        RuleFor(x => x.LoggedUserId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.UserId)
            .NotEmpty()
            .IsId();
    }
}
