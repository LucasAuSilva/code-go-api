using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Exercises.Queries.ResolveExercise;

public class ResolveExerciseValidator : AbstractValidator<ResolveExerciseQuery>
{
    public ResolveExerciseValidator()
    {
        RuleFor(x => x.ExerciseId).IsId();
        RuleFor(x => x.TestCaseId).IsId();
        RuleFor(x => x.SolutionCode).NotEmpty();
    }
}
