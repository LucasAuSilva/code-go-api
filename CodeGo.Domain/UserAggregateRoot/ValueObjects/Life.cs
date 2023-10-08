
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.UserAggregateRoot.ValueObjects;

public sealed class Life : ValueObject
{
    public int Count { get; private set; }
    public int Total { get; private set; }
    public DateTime LastRecharged { get; private set; }
    public DateTime LastLose { get; private set; }

    private Life(int total)
    {
        Count = total;
        Total = total;
        LastRecharged = DateTime.UtcNow;
        LastLose = DateTime.UtcNow;
    }

    public static Life CreateNew()
    {
        return new Life(5);
    }

    public void Recover()
    {
        Count = Total;
        LastRecharged = DateTime.UtcNow;
    }

    public void Lose()
    {
        Count -= 1;
        LastLose = DateTime.UtcNow;
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Count;
        yield return Total;
        yield return LastRecharged;
        yield return LastLose;
    }

#pragma warning disable CS8618
    private Life() {}
#pragma warning restore CS8618
}
