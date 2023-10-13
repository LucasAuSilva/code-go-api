
using System.Diagnostics.CodeAnalysis;
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.LessonTrackingAggregateRoot.Events;
using MediatR;

namespace CodeGo.Application.Progresses.Events;

public class FinishedLessonEventHandler : INotificationHandler<FinishedLessonEvent>
{
    private readonly IProgressRepository _progressRepository;
    private readonly ICourseRepository _courseRepository;

    public FinishedLessonEventHandler(
        IProgressRepository progressRepository,
        ICourseRepository courseRepository)
    {
        _progressRepository = progressRepository;
        _courseRepository = courseRepository;
    }

    public async Task Handle(
        FinishedLessonEvent notification,
        CancellationToken cancellationToken)
    {
        var progress = await _progressRepository.FindByUserIdAndCourseId(
            notification.LessonTracking.UserId,
            notification.LessonTracking.CourseId);
        if (progress is null)
            return;
        var course = await _courseRepository.FindById(notification.LessonTracking.CourseId);
        if (course is null)
            return;
        progress.UpdateModuleTracking(course, notification.LessonTracking.Status);
        await _progressRepository.UpdateAsync(progress);
    }
}
