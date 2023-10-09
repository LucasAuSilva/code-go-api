
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.LessonTrackingAggregateRoot.Entities;
using CodeGo.Domain.LessonTrackingAggregateRoot.Enums;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Domain.LessonTrackingAggregateRoot;

public sealed class LessonTracking : AggregateRoot<LessonTrackingId, Guid>
{
    private List<Practice> _practices = new();
    public UserId UserId { get; private set; }
    public CourseId CourseId { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public DateTime? EndDateTime { get; private set; }
    public LessonStatus Status { get; private set; }
    public IReadOnlyCollection<Practice> Practices => _practices;

    private LessonTracking(
        LessonTrackingId id,
        UserId userId,
        CourseId courseId,
        DateTime startDateTime,
        DateTime? endDateTime,
        LessonStatus status,
        List<Practice> practices
    ) : base(id)
    {
        UserId = userId;
        CourseId = courseId;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Status = status;
        _practices = practices;
    }

    public static LessonTracking CreateNew(
        UserId userId,
        CourseId courseId,
        List<Practice> practices)
    {
        return new LessonTracking(
            id: LessonTrackingId.CreateNew(),
            userId: userId,
            courseId: courseId,
            startDateTime: DateTime.UtcNow,
            endDateTime: null,
            status: LessonStatus.InProgress,
            practices: practices);
    }
}
