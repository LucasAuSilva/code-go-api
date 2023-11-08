
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Exercises.Queries.ListExerciseByCourse;

public class ListExerciseByCourseQueryValidator : AbstractValidator<ListExerciseByCourseQuery>
{
    public ListExerciseByCourseQueryValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .IsId();
    }
}
