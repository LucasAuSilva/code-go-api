
using Ardalis.SmartEnum;

namespace CodeGo.Domain.LessonTrackingAggregateRoot.Enums;

public sealed class LessonStatus : SmartEnum<LessonStatus>
{
    public static readonly LessonStatus Finished = new(nameof(Finished), 1);
    public static readonly LessonStatus Failed = new(nameof(Failed), 2);
    public static readonly LessonStatus InProgress = new(nameof(Cancelled), 3);
    public static readonly LessonStatus Cancelled = new(nameof(Cancelled), 4);

    public LessonStatus(string name, int value) : base(name, value)
    {}
}
