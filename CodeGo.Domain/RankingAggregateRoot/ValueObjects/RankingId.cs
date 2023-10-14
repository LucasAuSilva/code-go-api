
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.RankingAggregateRoot.ValueObjects;

public class RankingId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private RankingId(Guid value)
    {
        Value = value;
    }

    public static RankingId CreateNew()
    {
        return new RankingId(Guid.NewGuid());
    }

    public static RankingId Create(string value)
    {
        return new RankingId(Guid.Parse(value));
    }

    public static RankingId Create(Guid value)
    {
        return new RankingId(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private RankingId() {}
#pragma warning restore CS8618
}
