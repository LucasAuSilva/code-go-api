
using Ardalis.SmartEnum;

namespace CodeGo.Domain.LessonTrackingAggregateRoot.Enums;

public sealed class PracticeType : SmartEnum<PracticeType>
{
    public static readonly PracticeType Question = new(nameof(Question), 1);
    public static readonly PracticeType Exercise = new(nameof(Exercise), 2);

    public PracticeType(string name, int value) : base(name, value)
    {}
}
