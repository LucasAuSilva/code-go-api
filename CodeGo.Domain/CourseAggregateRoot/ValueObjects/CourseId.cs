
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.CourseAggregateRoot.ValueObjects;

public sealed class CourseId : ValueObject
{
    public Guid Value { get; }

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
