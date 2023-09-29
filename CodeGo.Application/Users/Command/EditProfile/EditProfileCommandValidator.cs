
using FluentValidation;

namespace CodeGo.Application.Users.Command.EditProfile;

public class EditProfileCommandValidator : AbstractValidator<EditProfileCommand>
{
    public EditProfileCommandValidator()
    {
        RuleFor(x => x.LoggedUserId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Visibility).NotEmpty();
    }
}
