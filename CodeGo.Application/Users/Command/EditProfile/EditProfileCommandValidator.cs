using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Users.Command.EditProfile;

public class EditProfileCommandValidator : AbstractValidator<EditProfileCommand>
{
    public EditProfileCommandValidator()
    {
        RuleFor(x => x.LoggedUserId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.UserId)
            .NotEmpty()
            .IsId()
            .Equal(x => x.LoggedUserId);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Visibility).NotEmpty();
    }
}
