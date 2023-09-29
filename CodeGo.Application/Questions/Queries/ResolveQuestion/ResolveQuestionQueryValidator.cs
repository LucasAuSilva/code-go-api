using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Questions.Queries.ResolveQuestion;

public class ResolveQuestionQueryValidator : AbstractValidator<ResolveQuestionQuery>
{
    public ResolveQuestionQueryValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.AlternativeId)
            .NotEmpty()
            .IsId();
    }
}
