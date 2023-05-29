
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.UserAggregateRoot.ValueObjects;

public sealed class Streak : ValueObject
{
    public int StreakCount { get; }
    public DateTime StreakLastUpdate { get; }

    private Streak(int streakCount, DateTime streakLastUpdate)
    {
        StreakCount = streakCount;
        StreakLastUpdate = streakLastUpdate;
    }

    public static Streak CreateNew()
    {
        return new Streak(0, DateTime.UtcNow);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return StreakCount;
        yield return StreakLastUpdate;
    }
}
