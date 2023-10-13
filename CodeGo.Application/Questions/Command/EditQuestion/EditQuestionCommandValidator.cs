
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Questions.Command.EditQuestion;

public class EditQuestionCommandValidator : AbstractValidator<EditQuestionCommand>
{
    public EditQuestionCommandValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.DifficultyValue).NotEmpty();
        RuleForEach(x => x.Alternatives)
            .NotEmpty()
            .SetValidator(new EditAlternativeCommandValidator());
    }
}

public class EditAlternativeCommandValidator : AbstractValidator<EditAlternativeCommand>
{
    public EditAlternativeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.Description)
            .NotEmpty();
        RuleFor(x => x.IsCorrect);
    }
}
