
using CodeGo.Domain.LessonTrackingAggregateRoot;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface ILessonTrackingRepository
{
    Task AddAsync(LessonTracking lessonTracking);
}
