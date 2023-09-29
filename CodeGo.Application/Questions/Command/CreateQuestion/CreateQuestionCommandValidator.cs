using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Questions.Command.CreateQuestion;

public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.DifficultyValue).NotEmpty();
        RuleForEach(x => x.Alternatives).SetValidator(new CreateAlternativeCommandValidator());
    }
}

public class CreateAlternativeCommandValidator : AbstractValidator<CreateAlternativeCommand>
{
    public CreateAlternativeCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.IsCorrect).NotEmpty();
    }
}
