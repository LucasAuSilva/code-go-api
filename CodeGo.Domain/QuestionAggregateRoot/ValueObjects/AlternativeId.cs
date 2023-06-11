
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.QuestionAggregateRoot.ValueObjects;

public sealed class AlternativeId : ValueObject
{
    public Guid Value { get; }

    private AlternativeId(Guid value)
    {
        Value = value;
    }

    public static AlternativeId CreateNew()
    {
        return new AlternativeId(Guid.NewGuid());
    }

    public static AlternativeId Create(Guid value)
    {
        return new AlternativeId(value);
    }

    public static AlternativeId Create(string value)
    {
        return new AlternativeId(Guid.Parse(value));
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
