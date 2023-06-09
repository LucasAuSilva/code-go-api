
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot;

namespace CodeGo.Infrastructure.Persistance;

public class CourseRepository : ICourseRepository
{
    private static readonly List<Course> _courses = new();

    public void Add(Course level)
    {
        _courses.Add(level);
    }
}
