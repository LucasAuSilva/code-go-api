
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Users.Command.UpdateUserRole;

public class UpdateUserRoleCommandValidator : AbstractValidator<UpdateUserRoleCommand>
{
    public UpdateUserRoleCommandValidator()
    {
        RuleFor(x => x.UserId)
            .Empty()
            .IsId();
        RuleFor(x => x.Role)
            .Empty()
            .LessThan(3);
    }
}
