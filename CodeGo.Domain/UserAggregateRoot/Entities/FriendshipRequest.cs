using CodeGo.Domain.Common.Models;
using CodeGo.Domain.UserAggregateRoot.Enums;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Domain.UserAggregateRoot.Entities;

public sealed class FriendshipRequest : AggregateRoot<FriendshipRequestId, Guid>
{
    public UserId Requester { get; }
    public string Message { get; }
    public FriendshipRequestStatus Status { get; private set; }

    private FriendshipRequest(
        FriendshipRequestId id,
        UserId requester,
        FriendshipRequestStatus status,
        string message) : base(id)
    {
        Requester = requester;
        Status = status;
        Message = message;
    }

    public static FriendshipRequest CreateNew(
        UserId requester,
        string? message
    )
    {
        message ??= "Hey lets code together!!";

        return new FriendshipRequest(
            id: FriendshipRequestId.CreateNew(),
            requester: requester,
            status: FriendshipRequestStatus.Pending,
            message: message
        );
    }

    public void Refused()
    {
        Status = FriendshipRequestStatus.Refused;
    }

    public void Blocked()
    {
        Status = FriendshipRequestStatus.Blocked;
    }

    public void Ignored()
    {
        Status = FriendshipRequestStatus.Ignored;
    }

    public void Accept()
    {
        Status = FriendshipRequestStatus.Accepted;
    }
}
