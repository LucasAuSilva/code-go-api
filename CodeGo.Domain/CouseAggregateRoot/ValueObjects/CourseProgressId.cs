
using CodeGo.Domain.Common.Models;

public sealed class CourseProgressId : ValueObject
{
    public Guid Value { get; }

    private CourseProgressId(Guid value)
    {
        Value = value;
    }

    public static CourseProgressId CreateNew()
    {
        return new CourseProgressId(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
