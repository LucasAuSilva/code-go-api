
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

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
