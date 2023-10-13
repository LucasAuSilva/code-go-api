
using CodeGo.Domain.LessonTrackingAggregateRoot;
using CodeGo.Domain.LessonTrackingAggregateRoot.Enums;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface ILessonTrackingRepository
{
    Task AddAsync(LessonTracking lessonTracking);
    Task<List<LessonTracking>> FindByStatus(LessonStatus status);
    Task UpdateManyAsync(List<LessonTracking> lessons);
    Task<LessonTracking?> FindByIdAndUserId(LessonTrackingId id, UserId userId);
    Task UpdateAsync(LessonTracking lessonTracking);
}
