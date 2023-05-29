
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.LevelAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Domain.LevelAggregateRoot;

public sealed class Level : AggregateRoot<LevelId>
{
    public int NumberRepresentation { get; }
    public ExperiencePoints MinimumPoints { get; }
    public LevelId? NextLevel { get; }
    public LevelId? PreviousLevel { get; }

    private Level(
        LevelId id,
        int numberRepresentation,
        ExperiencePoints minimumPoints,
        LevelId? nextLevel = null,
        LevelId? previousLevel = null) : base(id)
    {
        NumberRepresentation = numberRepresentation;
        MinimumPoints = minimumPoints;
        NextLevel = nextLevel;
        PreviousLevel = previousLevel;
    }

    public static Level CreateNew(
        int number,
        int minimumPoints)
    {
        var points = ExperiencePoints.Create(minimumPoints);
        return new Level(
            id: LevelId.CreateNew(),
            numberRepresentation: number,
            minimumPoints: points);
    }
}
