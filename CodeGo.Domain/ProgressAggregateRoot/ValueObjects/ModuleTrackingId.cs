
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.ProgressAggregateRoot.ValueObjects;

public sealed class ModuleTrackingId : ValueObject
{
    public Guid Value { get; private set; }

    private ModuleTrackingId(Guid value)
    {
        Value = value;
    }

    public static ModuleTrackingId CreateNew()
    {
        return new ModuleTrackingId(Guid.NewGuid());
    }

    public static ModuleTrackingId Create(string value)
    {
        return new ModuleTrackingId(Guid.Parse(value));
    }

    public static ModuleTrackingId Create(Guid value)
    {
        return new ModuleTrackingId(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private ModuleTrackingId() {}
#pragma warning restore CS8618
}
