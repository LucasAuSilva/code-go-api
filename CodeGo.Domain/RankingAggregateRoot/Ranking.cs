
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.RankingAggregateRoot.Entities;
using CodeGo.Domain.RankingAggregateRoot.Events;
using CodeGo.Domain.RankingAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot;
using ErrorOr;

namespace CodeGo.Domain.RankingAggregateRoot;

public sealed class Ranking : AggregateRoot<RankingId, Guid>
{
    private List<RankingProgress> _rankingProgresses = new();
    public CourseId CourseId { get; private set; }
    public Period Period { get; private set; } 
    public IReadOnlyCollection<RankingProgress> RankingProgresses => _rankingProgresses;

    private Ranking(
        RankingId id,
        CourseId courseId,
        Period period) : base(id)
    {
        CourseId = courseId;
        Period = period;
    }

    public static Ranking CreateNew(
        CourseId courseId)
    {
        var period = Period.TilNextSunday();
        var ranking = new Ranking(
            RankingId.CreateNew(),
            courseId,
            period);
        ranking.AddDomainEvent(new EndedRankingPeriodEvent(ranking));
        return ranking;
    }

    public override RankingId IdToValueObject()
    {
        return RankingId.Create(Id.Value);
    }

    public void UpdateUserRankingProgress(User user, bool isCorrect, Difficulty difficulty)
    {
        var userId = user.IdToValueObject();
        var progress = _rankingProgresses.Find(rp => rp.UserId == userId);
        if (progress is null)
        {
            progress = RankingProgress.CreateNew(userId, user.FullName);
            _rankingProgresses.Add(progress);
        }
        if (isCorrect)
            progress.IncreasePoints(difficulty);
        return;
    }

    public void EndRanking()
    {
        var finalists = _rankingProgresses
            .OrderBy(rp => rp.Points)
            .Take(3)
            .Select(ranking => ranking.UserId)
            .ToList();
        var position = 1;
        foreach (var finalist in finalists)
        {
            AddDomainEvent(new FinishedRankingAsFinalist(finalist, position));
            position++;
        }
    }

    public ErrorOr<Ranking> Ordered()
    {
        _rankingProgresses = _rankingProgresses.OrderBy(rp => rp.Points).ToList();
        return this;
    }

#pragma warning disable CS8618
    private Ranking() {}
#pragma warning restore CS8618
}
