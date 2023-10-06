
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.UserAggregateRoot.ValueObjects;

public sealed class Life : ValueObject
{
    public int LifeCount { get; private set; }
    public DateTime LastRecharged { get; private set; }
    public DateTime LastLose { get; private set; }

    private Life(int lifeCount)
    {
        LifeCount = lifeCount;
        LastRecharged = DateTime.UtcNow;
        LastLose = DateTime.UtcNow;
    }

    public static Life CreateNew()
    {
        return new Life(5);
    }

    public void Recover()
    {
        LifeCount = 5;
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return LifeCount;
        yield return LastRecharged;
        yield return LastLose;
    }

#pragma warning disable CS8618
    private Life() {}
#pragma warning restore CS8618
}
