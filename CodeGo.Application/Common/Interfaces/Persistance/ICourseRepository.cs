
namespace CodeGo.Application.Common.Interfaces.Persistance;

using CodeGo.Domain.CourseAggregateRoot;

public interface ICourseRepository
{
    void Add(Course course);
    Course? FindById(Guid courseId);
}
