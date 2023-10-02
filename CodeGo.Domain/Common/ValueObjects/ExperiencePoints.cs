
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.UserAggregateRoot.ValueObjects;

public sealed class ExperiencePoints : ValueObject
{
    public int Points { get; }

    private ExperiencePoints(int points)
    {
        Points = points;
    }

    public static ExperiencePoints CreateNew()
    {
        return new ExperiencePoints(0);
    }

    public static ExperiencePoints Create(int points)
    {
        return new ExperiencePoints(points);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Points;
    }

#pragma warning disable CS8618
    private ExperiencePoints() {}
#pragma warning restore CS8618
}
