
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Progresses.Queries.GetProgressDetails;

public class GetProgressDetailsQueryValidator : AbstractValidator<GetProgressDetailsQuery>
{
    public GetProgressDetailsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .IsId();
    }
}
