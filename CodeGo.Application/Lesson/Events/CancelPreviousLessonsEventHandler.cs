
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.LessonTrackingAggregateRoot.Enums;
using CodeGo.Domain.LessonTrackingAggregateRoot.Events;
using MediatR;

namespace CodeGo.Application.Lesson.Events;

public class CancelPreviousLessonsEventHandler : INotificationHandler<CancelPreviousLessonsEvent>
{
    private readonly ILessonTrackingRepository _lessonTrackingRepository;

    public CancelPreviousLessonsEventHandler(
        ILessonTrackingRepository lessonTrackingRepository)
    {
        _lessonTrackingRepository = lessonTrackingRepository;
    }

    public async Task Handle(CancelPreviousLessonsEvent notification, CancellationToken cancellationToken)
    {
        var lessonsInProgress = await _lessonTrackingRepository.FindByStatus(LessonStatus.InProgress);
        if (lessonsInProgress.Count == 1)
            return;
        var notRecentCreatedLessons = lessonsInProgress
            .Where(lesson => !lesson.Id.Equals(notification.ActiveLessonTrackingId))
            .ToList();
        notRecentCreatedLessons.ForEach(lesson => lesson.CancelLesson());
        await _lessonTrackingRepository.UpdateManyAsync(notRecentCreatedLessons);
    }
}
