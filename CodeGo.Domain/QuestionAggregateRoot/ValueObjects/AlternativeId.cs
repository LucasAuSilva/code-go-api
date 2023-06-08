
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

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
