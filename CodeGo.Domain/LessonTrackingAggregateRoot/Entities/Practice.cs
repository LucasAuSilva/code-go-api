
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.LessonTrackingAggregateRoot.Enums;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;

namespace CodeGo.Domain.LessonTrackingAggregateRoot.Entities;

public sealed class Practice : Entity<PracticeId>
{
    public string AnswerId { get; private set; }
    public bool IsCorrect { get; private set; }
    public PracticeType Type { get; private set; }

    private Practice(
        PracticeId id,
        string answerId,
        bool isCorrect,
        PracticeType type) : base(id)
    {
        AnswerId = answerId;
        IsCorrect = isCorrect;
        Type = type;
    }

    public static Practice CreateNew(
        string answerId,
        bool isCorrect,
        PracticeType type)
    {
        return new Practice(
            PracticeId.CreateNew(),
            answerId,
            isCorrect,
            type);
    }
}
