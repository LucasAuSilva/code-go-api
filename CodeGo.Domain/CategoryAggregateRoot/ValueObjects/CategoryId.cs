
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.CategoryAggregateRoot.ValueObjects;

public sealed class CategoryId : ValueObject
{
    public Guid Value { get; }

    private CategoryId(Guid value)
    {
        Value = value;
    }

    public static CategoryId CreateNew()
    {
        return new CategoryId(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
