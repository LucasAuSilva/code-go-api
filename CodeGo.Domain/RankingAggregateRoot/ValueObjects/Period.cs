
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.RankingAggregateRoot.ValueObjects;

public sealed class Period : ValueObject
{
    public DateTime InitialDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }

    private Period(DateTime initialDateTime, DateTime endDateTime)
    {
        InitialDateTime = initialDateTime;
        EndDateTime = endDateTime;
    }

    public static Period CreateNew(DateTime initialDateTime, DateTime endDateTime)
    {
        return new Period(initialDateTime, endDateTime);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return InitialDateTime;
        yield return EndDateTime;
    }

#pragma warning disable CS8618
    private Period() {}
#pragma warning restore CS8618
}
