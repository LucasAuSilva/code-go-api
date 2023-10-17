
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Exercises.Command.DeleteExercise;

public class DeleteExerciseCommandValidator : AbstractValidator<DeleteExerciseCommand>
{
    public DeleteExerciseCommandValidator()
    {
        RuleFor(x => x.ExerciseId)
            .NotEmpty()
            .IsId();
    }
}
