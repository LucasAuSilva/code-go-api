
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.RankingAggregateRoot.Entities;
using CodeGo.Domain.RankingAggregateRoot.ValueObjects;

namespace CodeGo.Domain.RankingAggregateRoot;

public sealed class Ranking : AggregateRoot<RankingId, Guid>
{
    private List<RankingProgress> _rankingProgresses = new();
    public CourseId CourseId { get; private set; }
    public Period Period { get; private set; } 
    public IReadOnlyCollection<RankingProgress> RankingProgresses => _rankingProgresses;

    public override RankingId IdToValueObject()
    {
        return RankingId.Create(Id.Value);
    }

#pragma warning disable CS8618
    private Ranking() {}
#pragma warning restore CS8618
}
