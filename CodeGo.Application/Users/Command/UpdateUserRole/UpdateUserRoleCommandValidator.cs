
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Users.Command.UpdateUserRole;

public class UpdateUserRoleCommandValidator : AbstractValidator<UpdateUserRoleCommand>
{
    public UpdateUserRoleCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.Role)
            .NotEmpty()
            .LessThan(3);
    }
}
