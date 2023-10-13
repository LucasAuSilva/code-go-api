
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Lesson.Command.FinishLesson;

public class FinishLessonCommandValidator : AbstractValidator<FinishLessonCommand>
{
    public FinishLessonCommandValidator()
    {
        RuleFor(x => x.LessonTrackingId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.UserId)
            .NotEmpty()
            .IsId();
    }
}
