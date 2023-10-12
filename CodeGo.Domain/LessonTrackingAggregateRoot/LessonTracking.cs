
using System.Runtime.CompilerServices;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.LessonTrackingAggregateRoot.Entities;
using CodeGo.Domain.LessonTrackingAggregateRoot.Enums;
using CodeGo.Domain.LessonTrackingAggregateRoot.Events;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;

namespace CodeGo.Domain.LessonTrackingAggregateRoot;

public sealed class LessonTracking : AggregateRoot<LessonTrackingId, Guid>
{
    private List<Practice> _practices = new();
    public UserId UserId { get; private set; }
    public CourseId CourseId { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public DateTime? EndDateTime { get; private set; }
    public LessonStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyCollection<Practice> Practices => _practices;

    private LessonTracking(
        LessonTrackingId id,
        UserId userId,
        CourseId courseId,
        DateTime startDateTime,
        DateTime? endDateTime,
        LessonStatus status,
        List<Practice> practices,
        DateTime createdAt,
        DateTime updatedAt) : base(id)
    {
        UserId = userId;
        CourseId = courseId;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Status = status;
        _practices = practices;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static LessonTracking CreateNew(
        UserId userId,
        CourseId courseId,
        List<Practice> practices)
    {
        var id = LessonTrackingId.CreateNew(); 
        var lessonTracking = new LessonTracking(
            id: id,
            userId: userId,
            courseId: courseId,
            startDateTime: DateTime.UtcNow,
            endDateTime: null,
            status: LessonStatus.InProgress,
            practices: practices,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow);
        lessonTracking.AddDomainEvent(new CancelPreviousLessonsEvent(id));
        return lessonTracking;
    }

    public ErrorOr<Success> ResolvePractice(
        string activityId,
        string answerId,
        bool isCorrect,
        Difficulty difficulty,
        UserId userId)
    {
        var practice = _practices.FirstOrDefault(practice => practice.ActivityId.Equals(activityId));
        if (practice is null)
            return Errors.LessonTrackings.PracticeNotFoundByActivity;
        AddDomainEvent(new ResolvedPracticeEvent(practice, difficulty, userId));
        return practice.Resolve(answerId, isCorrect);
    }

    // TODO: Maybe make user lost ranking points for cancel lessons
    public void CancelLesson()
    {
        if (Status == LessonStatus.InProgress)
        {
            Status = LessonStatus.Cancelled;
            EndDateTime = DateTime.UtcNow;
        }
    }

    public override LessonTrackingId IdToValueObject()
    {
        return LessonTrackingId.Create(Id.Value);
    }

#pragma warning disable CS8618
    private LessonTracking() {}
#pragma warning restore CS8618
}
