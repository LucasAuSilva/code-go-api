
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.UserAggregateRoot.ValueObjects;

public sealed class UserId : ValueObject
{
    public Guid Value { get; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId CreateNew()
    {
        return new UserId(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
