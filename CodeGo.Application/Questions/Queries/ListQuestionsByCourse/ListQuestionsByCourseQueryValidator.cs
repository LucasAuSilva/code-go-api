
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Questions.Queries.ListQuestionsByCourse;

public class ListQuestionsByCourseQueryValidator : AbstractValidator<ListQuestionsByCourseQuery>
{
    public ListQuestionsByCourseQueryValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .IsId();
    }
}
