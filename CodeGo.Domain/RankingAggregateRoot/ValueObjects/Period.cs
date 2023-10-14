
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.Utils;

namespace CodeGo.Domain.RankingAggregateRoot.ValueObjects;

public sealed class Period : ValueObject
{
    public DateTime InitialDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }
    public bool IsGoingToReset { get; private set; }

    private Period(DateTime initialDateTime, DateTime endDateTime)
    {
        InitialDateTime = initialDateTime;
        EndDateTime = endDateTime;
    }

    public static Period CreateNew(DateTime initialDateTime, DateTime endDateTime)
    {
        return new Period(initialDateTime, endDateTime);
    }

    public static Period TilNextSunday()
    {
        var today = DateTime.UtcNow;
        var nextSunday = today.Next(DayOfWeek.Sunday);
        return new Period(today, nextSunday);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return InitialDateTime;
        yield return EndDateTime;
    }

    public int InMinutes()
    {
        return (EndDateTime - DateTime.Now).Minutes;
    }

#pragma warning disable CS8618
    private Period() {}
#pragma warning restore CS8618
}
