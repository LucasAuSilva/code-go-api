
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ProgressAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IProgressRepository
{
    Task AddAsync(Progress progress);
    Task<Progress?> FindByUserIdAndCourseId(UserId userId, CourseId courseId);
}
