
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.CategoryAggregateRoot.ValueObjects;

public sealed class CategoryId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private CategoryId(Guid value)
    {
        Value = value;
    }

    public static CategoryId CreateNew()
    {
        return new CategoryId(Guid.NewGuid());
    }

    public static CategoryId Create(Guid value)
    {
        return new CategoryId(value);
    }

    public static CategoryId Create(string value)
    {
        return new CategoryId(Guid.Parse(value));
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private CategoryId() {}
#pragma warning restore CS8618
}
