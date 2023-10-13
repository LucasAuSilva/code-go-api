
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Courses.Queries.FindCourse;

public class FindCourseQueryHandler : IRequestHandler<FindCourseQuery, ErrorOr<Course>>
{
    private readonly ICourseRepository _courseRepository;

    public FindCourseQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<ErrorOr<Course>> Handle(
        FindCourseQuery query,
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.FindById(CourseId.Create(query.CourseId));
        if (course is null)
            return Errors.Course.CourseNotFound;
        return course;
    }
}
