
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.CouseAggregateRoot.ValueObjects;

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

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
