
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.CourseAggregateRoot.ValueObjects;

public sealed class LanguageId : ValueObject
{
    public Guid Value { get; }

    private LanguageId(Guid value)
    {
        Value = value;
    }

    public static LanguageId CreateNew()
    {
        return new LanguageId(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
