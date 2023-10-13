
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using CodeGo.Domain.ProgressAggregateRoot;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IProgressRepository
{
    Task AddAsync(Progress progress);
    Task<Progress?> FindByUserIdAndCourseId(UserId userId, CourseId courseId);
    Task UpdateAsync(Progress progress);
}
