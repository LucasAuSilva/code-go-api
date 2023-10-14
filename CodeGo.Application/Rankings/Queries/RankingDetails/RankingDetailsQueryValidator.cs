

using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Rankings.Queries;

public class RankingDetailsQueryValidator : AbstractValidator<RankingDetailsQuery>
{
    public RankingDetailsQueryValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .IsId();
    }
}
