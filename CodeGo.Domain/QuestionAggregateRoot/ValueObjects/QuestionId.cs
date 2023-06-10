
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.QuestionAggregateRoot.ValueObjects;

public sealed class QuestionId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private QuestionId(Guid value)
    {
        Value = value;
    }

    public static QuestionId CreateNew()
    {
        return new QuestionId(Guid.NewGuid());
    }

    public static QuestionId Create(Guid value)
    {
        return new QuestionId(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
