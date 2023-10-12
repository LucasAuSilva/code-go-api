
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.ProgressAggregateRoot.ValueObjects;

public sealed class CategoryTrackingId : ValueObject
{
    public Guid Value { get; private set; }

    private CategoryTrackingId(Guid value)
    {
        Value = value;
    }

    public static CategoryTrackingId CreateNew()
    {
        return new CategoryTrackingId(Guid.NewGuid());
    }

    public static CategoryTrackingId Create(string value)
    {
        return new CategoryTrackingId(Guid.Parse(value));
    }

    public static CategoryTrackingId Create(Guid value)
    {
        return new CategoryTrackingId(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private CategoryTrackingId() {}
#pragma warning restore CS8618
}
