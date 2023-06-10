
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.CourseAggregateRoot.ValueObjects;

public sealed class CourseId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private CourseId(Guid value)
    {
        Value = value;
    }

    public static CourseId CreateNew()
    {
        return new CourseId(Guid.NewGuid());
    }

    public static CourseId Create(string value)
    {
        return new CourseId(Guid.Parse(value));
    }

    public static CourseId Create(Guid value)
    {
        return new CourseId(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
