
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Exercises.Command.EditExercise;

public class EditExerciseCommandValidator : AbstractValidator<EditExerciseCommand>
{
    public EditExerciseCommandValidator()
    {
        RuleFor(x => x.ExerciseId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.DifficultyValue).NotEmpty();
        RuleFor(x => x.TypeValue).NotEmpty();
        RuleForEach(x => x.TestCases)
            .NotEmpty()
            .SetValidator(new EditTestCaseCommandValidator());
    }
}

public class EditTestCaseCommandValidator : AbstractValidator<EditTestCaseCommand>
{
    public EditTestCaseCommandValidator()
    {
        RuleFor(x => x.TestCaseId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Result).NotEmpty();
    }
}
