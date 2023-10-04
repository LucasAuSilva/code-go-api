
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.UserAggregateRoot.ValueObjects;

public sealed class Streak : ValueObject
{
    public int StreakCount { get; private set; }
    public DateTime StreakLastUpdate { get; private set; }

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

#pragma warning disable CS8618
    private Streak() {}
#pragma warning restore CS8618
}
