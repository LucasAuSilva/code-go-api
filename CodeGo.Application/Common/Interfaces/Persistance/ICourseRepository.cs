
namespace CodeGo.Application.Common.Interfaces.Persistance;

using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;

public interface ICourseRepository
{
    Task<bool> Exists(CourseId courseId);
    Task Add(Course course);
    Task<Course?> FindById(CourseId courseId);
    Task Update(Course course);
    Task<List<Course>> ListAsync();
}
