
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Courses.Queries.FindCourse;

public class FindCourseQueryValidator : AbstractValidator<FindCourseQuery>
{
    public FindCourseQueryValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .IsId();
    }
}
