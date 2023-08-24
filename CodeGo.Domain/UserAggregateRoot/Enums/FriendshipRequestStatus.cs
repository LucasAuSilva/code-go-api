
using System.ComponentModel;
using Ardalis.SmartEnum;

namespace CodeGo.Domain.UserAggregateRoot.Enums;

public sealed class FriendshipRequestStatus : SmartEnum<FriendshipRequestStatus>
{
    public static readonly FriendshipRequestStatus Pending = new (nameof(Pending), 1);
    public static readonly FriendshipRequestStatus Accepted = new (nameof(Accepted), 2);
    public static readonly FriendshipRequestStatus Refused = new (nameof(Refused), 3);
    public static readonly FriendshipRequestStatus Ignored = new (nameof(Ignored), 4);
    public static readonly FriendshipRequestStatus Blocked = new (nameof(Blocked), 5);

    public FriendshipRequestStatus(string Name, int Value) : base(Name, Value)
    {}
}
