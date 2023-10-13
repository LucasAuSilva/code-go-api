
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Lesson.Command.ResolveQuestion;

public class ResolveQuestionCommandValidator : AbstractValidator<ResolveQuestionCommand>
{
    public ResolveQuestionCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.LessonTrackingId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.QuestionId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.AlternativeId)
            .NotEmpty()
            .IsId();
    }
}
