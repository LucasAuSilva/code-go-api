
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.CourseAggregateRoot.ValueObjects;

public sealed class SectionId : ValueObject
{
    public Guid Value { get; }

    private SectionId(Guid value)
    {
        Value = value;
    }

    public static SectionId CreateNew()
    {
        return new SectionId(Guid.NewGuid());
    }

    public static SectionId Create(Guid value)
    {
        return new SectionId(value);
    }

    public static SectionId Create(string value)
    {
        return new SectionId(Guid.Parse(value));
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
