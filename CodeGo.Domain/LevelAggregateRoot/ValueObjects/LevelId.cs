
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.LevelAggregateRoot.ValueObjects;

public sealed class LevelId : ValueObject
{
    public Guid Value { get; }

    private LevelId(Guid value)
    {
        Value = value;
    }

    public static LevelId CreateNew()
    {
        return new LevelId(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
