
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.ProgressAggregateRoot.ValueObjects;

public sealed class ProgressId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private ProgressId(Guid value)
    {
        Value = value;
    }

    public static ProgressId CreateNew()
    {
        return new ProgressId(Guid.NewGuid());
    }

    public static ProgressId Create(string value)
    {
        return new ProgressId(Guid.Parse(value));
    }

    public static ProgressId Create(Guid value)
    {
        return new ProgressId(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private ProgressId() {}
#pragma warning restore CS8618
}
