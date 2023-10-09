
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;

public sealed class LessonTrackingId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private LessonTrackingId(Guid value)
    {
        Value = value;
    }

    public static LessonTrackingId CreateNew()
    {
        return new LessonTrackingId(Guid.NewGuid());
    }

    public static LessonTrackingId Create(string value)
    {
        return new LessonTrackingId(Guid.Parse(value));
    }

    public static LessonTrackingId Create(Guid value)
    {
        return new LessonTrackingId(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private LessonTrackingId() {}
#pragma warning restore CS8618

}
