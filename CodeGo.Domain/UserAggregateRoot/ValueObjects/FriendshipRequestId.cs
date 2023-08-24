
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.UserAggregateRoot.ValueObjects;

public sealed class FriendshipRequestId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

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
