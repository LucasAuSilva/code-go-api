
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Lesson.Command.ResolveExercise;

public class ResolveExerciseCommandValidator : AbstractValidator<ResolveExerciseCommand>
{
    public ResolveExerciseCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.LessonTrackingId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.ExerciseId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.TestCaseId)
            .NotEmpty()
            .IsId();
    }
}
