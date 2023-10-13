
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

    public void CountStreak()
    {
        var today = DateTime.UtcNow.Date;
        var lastUpdateDate = StreakLastUpdate.Date;
        if (today > lastUpdateDate && today.AddDays(-1).Date == lastUpdateDate.Date)
        {
            StreakCount++;
            StreakLastUpdate = DateTime.UtcNow;
            return;
        }
        if (today == lastUpdateDate)
            return;
        StreakCount = 1;
        StreakLastUpdate = DateTime.UtcNow;
        return;
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
