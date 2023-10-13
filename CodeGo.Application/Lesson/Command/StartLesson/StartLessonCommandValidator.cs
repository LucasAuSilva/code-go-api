
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Lesson.Command.StartLesson;

public class StartLessonCommandValidator : AbstractValidator<StartLessonCommand>
{
    public StartLessonCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.ModuleId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.UserId)
            .NotEmpty()
            .IsId();
    }
}
