
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.RankingAggregateRoot.ValueObjects;

public sealed class RankingProgressId : ValueObject
{
    public Guid Value { get; }

    private RankingProgressId(Guid value)
    {
        Value = value;
    }

    public static RankingProgressId CreateNew()
    {
        return new RankingProgressId(Guid.NewGuid());
    }

    public static RankingProgressId Create(string value)
    {
        return new RankingProgressId(Guid.Parse(value));
    }

    public static RankingProgressId Create(Guid value)
    {
        return new RankingProgressId(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private RankingProgressId() {}
#pragma warning restore CS8618
}
