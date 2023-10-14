
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;

namespace CodeGo.Domain.UserAggregateRoot.ValueObjects;

public sealed class ExperiencePoints : ValueObject
{
    public int Points { get; private set; }

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

    public void CalculatePointsByDifficulty(Difficulty difficulty) 
    {
        Points += 10 * difficulty.Value;
    }

    public void Increase(ExperiencePoints points)
    {
        Points += points.Points;
    }

    public void CalculatePointsByPosition(int position)
    {
        switch(position) {
            case 1:
                Points += 1000;
                break;
            case 2:
                Points += 500;
                break;
            case 3:
                Points += 250;
                break;
            default:
                break;
        }
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Points;
    }

#pragma warning disable CS8618
    private ExperiencePoints() {}
#pragma warning restore CS8618
}
