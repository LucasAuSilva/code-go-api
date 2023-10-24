
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Courses.Queries.ListCourses;

public class ListCoursesQueryHandler : IRequestHandler<ListCoursesQuery, ErrorOr<List<Course>>>
{
    private readonly ICourseRepository _courseRepository;

    public ListCoursesQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<ErrorOr<List<Course>>> Handle(ListCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.ListAsync();
        courses = courses.Select(course =>
            {
                course.OrderPositions();
                return course;
            })
            .ToList();
        return courses;
    }
}
