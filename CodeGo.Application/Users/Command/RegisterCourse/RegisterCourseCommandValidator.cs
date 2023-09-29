using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Users.Command.RegisterCourse;

public class RegisterCourseCommandValidator : AbstractValidator<RegisterCourseCommand>
{
    public RegisterCourseCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .IsId();
    }
}
