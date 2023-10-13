
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Questions.Command.DeleteQuestion;

public class DeleteQuestionCommandValidator : AbstractValidator<DeleteQuestionCommand>
{
    public DeleteQuestionCommandValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEmpty()
            .IsId();
    }
}
