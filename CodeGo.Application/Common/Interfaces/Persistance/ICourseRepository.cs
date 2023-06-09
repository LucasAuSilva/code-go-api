
namespace CodeGo.Application.Common.Interfaces.Persistance;

using CodeGo.Domain.CourseAggregateRoot;

public interface ICourseRepository
{
    Task Add(Course course);
    Task<Course?> FindById(Guid courseId);
}
