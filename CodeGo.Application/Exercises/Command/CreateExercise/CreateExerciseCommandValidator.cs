using System.Security.Cryptography.Xml;
using FluentValidation;

namespace CodeGo.Application.Exercises.Command.CreateExercise;

public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
{
    public CreateExerciseCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.BaseCode).NotEmpty();
        RuleFor(x => x.DifficultyValue).NotEmpty();
        RuleFor(x => x.TypeValue).NotEmpty();
        RuleFor(x => x.CourseId).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleForEach(x => x.TestCases).SetValidator(new CreateTestCaseCommandValidator());
    }
}

public class CreateTestCaseCommandValidator : AbstractValidator<CreateTestCaseCommand>
{
    public CreateTestCaseCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Result).NotEmpty();
    }
}
