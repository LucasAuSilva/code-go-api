
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.RankingAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Domain.RankingAggregateRoot.Entities;

public sealed class RankingProgress : Entity<RankingProgressId>
{
    public UserId UserId { get; private set; }
    public string UserFullName { get; private set; }
    public ExperiencePoints Points { get; private set; }

    private RankingProgress(
        RankingProgressId id,
        UserId userId,
        string userFullName,
        ExperiencePoints points) : base(id)
    {
        UserId = userId;
        UserFullName = userFullName;
        Points = points;
    }

    public static RankingProgress CreateNew(
        UserId userId,
        string userFullName)
    {
        var points = ExperiencePoints.CreateNew();
        return new RankingProgress(
            RankingProgressId.CreateNew(),
            userId,
            userFullName,
            points);
    }

    public void IncreasePoints(Difficulty difficulty)
    {
        Points.CalculatePointsByDifficulty(difficulty);
    }

#pragma warning disable CS8618
    private RankingProgress() {}
#pragma warning restore CS8618
}
