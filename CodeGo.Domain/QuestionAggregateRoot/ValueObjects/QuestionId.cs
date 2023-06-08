
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.QuestionAggregateRoot.ValueObjects;

public sealed class QuestionId : ValueObject
{
    public Guid Value { get; }

    private QuestionId(Guid value)
    {
        Value = value;
    }

    public static QuestionId CreateNew()
    {
        return new QuestionId(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
