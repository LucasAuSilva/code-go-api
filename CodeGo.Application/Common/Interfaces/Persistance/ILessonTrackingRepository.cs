
using CodeGo.Domain.LessonTrackingAggregateRoot;
using CodeGo.Domain.LessonTrackingAggregateRoot.Enums;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface ILessonTrackingRepository
{
    Task AddAsync(LessonTracking lessonTracking);
    Task<List<LessonTracking>> FindByStatus(LessonStatus status);
    Task UpdateManyAsync(List<LessonTracking> lessons);
}
