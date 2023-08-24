
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.UserAggregateRoot.ValueObjects;

public sealed class FriendshipRequestId : ValueObject
{
    public Guid Value { get; private set; }

    private FriendshipRequestId(Guid value)
    {
        Value = value;
    }

    public static FriendshipRequestId CreateNew()
    {
        return new FriendshipRequestId(Guid.NewGuid());
    }

    public static FriendshipRequestId Create(string value)
    {
        return new FriendshipRequestId(Guid.Parse(value));
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
