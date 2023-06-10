
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.LevelAggregateRoot.ValueObjects;

public sealed class LevelId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private LevelId(Guid value)
    {
        Value = value;
    }

    public static LevelId CreateNew()
    {
        return new LevelId(Guid.NewGuid());
    }

    public static LevelId Create(Guid value)
    {
        return new LevelId(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
