
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;

public sealed class PracticeId : ValueObject
{
    public Guid Value { get; }

    private PracticeId(Guid value)
    {
        Value = value;
    }

    public static PracticeId CreateNew()
    {
        return new PracticeId(Guid.NewGuid());
    }

    public static PracticeId Create(string value)
    {
        return new PracticeId(Guid.Parse(value));
    }

    public static PracticeId Create(Guid value)
    {
        return new PracticeId(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private PracticeId() {}
#pragma warning restore CS8618
}
