
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ProgressAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.Events;
using MediatR;

namespace CodeGo.Application.Courses.Events;

public class RegisteredNewCourseEventHandler : INotificationHandler<RegisteredNewCourseEvent>
{
    private readonly IProgressRepository _progressRepository;

    public RegisteredNewCourseEventHandler(IProgressRepository progressRepository)
    {
        _progressRepository = progressRepository;
    }

    public async Task Handle(
        RegisteredNewCourseEvent notification,
        CancellationToken cancellationToken)
    {
        var progress = Progress.CreateNew(
            notification.UserId,
            CourseId.Create(notification.Course.Id.Value),
            notification.Course.FirstModule(),
            notification.Course.FirstSection());
        await _progressRepository.AddAsync(progress);
    }
}
