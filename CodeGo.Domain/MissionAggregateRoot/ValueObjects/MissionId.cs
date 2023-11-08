
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.MissionAggregateRoot.ValueObjects;

public class MissionId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private MissionId(Guid value)
    {
        Value = value;
    }

    public static MissionId CreateNew()
    {
        return new MissionId(Guid.NewGuid());
    }

    public static MissionId Create(string value)
    {
        return new MissionId(Guid.Parse(value));
    }

    public static MissionId Create(Guid value)
    {
        return new MissionId(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private MissionId() {}
#pragma warning restore CS8618
}
