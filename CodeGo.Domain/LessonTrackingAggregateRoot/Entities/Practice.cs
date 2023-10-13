
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.LessonTrackingAggregateRoot.Enums;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;
using ErrorOr;

namespace CodeGo.Domain.LessonTrackingAggregateRoot.Entities;

public sealed class Practice : Entity<PracticeId>
{
    public string ActivityId { get; private set; }
    public string? AnswerId { get; private set; }
    public bool IsCorrect { get; private set; }
    public PracticeType Type { get; private set; }

    private Practice(
        PracticeId id,
        string activityId,
        string? answerId,
        bool isCorrect,
        PracticeType type) : base(id)
    {
        ActivityId = activityId;
        AnswerId = answerId;
        IsCorrect = isCorrect;
        Type = type;
    }

    public static Practice CreateNew(
        string activityId,
        PracticeType type)
    {
        return new Practice(
            PracticeId.CreateNew(),
            activityId,
            null,
            false,
            type);
    }

    public ErrorOr<Success> Resolve(string answerId, bool isCorrect)
    {
        if (AnswerId is not null)
            return Errors.LessonTrackings.PracticeAlreadyAnswered;
        AnswerId = answerId;
        IsCorrect = isCorrect;
        return Result.Success;
    }

#pragma warning disable CS8618
    private Practice() {}
#pragma warning restore CS8618
}
