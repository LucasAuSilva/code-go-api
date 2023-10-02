using CodeGo.Domain.Common.Models;
using CodeGo.Domain.UserAggregateRoot.Enums;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Domain.UserAggregateRoot.Entities;

public sealed class FriendshipRequest : Entity<FriendshipRequestId>
{
    public UserId RequesterId { get; }
    public string RequesterEmail { get; }
    public string? RequesterPhoto { get; }
    public string Message { get; }
    public FriendshipRequestStatus Status { get; private set; }

    private FriendshipRequest(
        FriendshipRequestId id,
        string requesterEmail,
        string? requesterPhoto,
        UserId requesterId,
        FriendshipRequestStatus status,
        string message) : base(id)
    {
        RequesterId = requesterId;
        RequesterEmail = requesterEmail;
        RequesterPhoto = requesterPhoto;
        Status = status;
        Message = message;
    }

    public static FriendshipRequest CreateNew(
        UserId requesterId,
        string requesterEmail,
        string? requesterPhoto,
        string? message
    )
    {
        message ??= "Hey lets code together!!";

        return new FriendshipRequest(
            id: FriendshipRequestId.CreateNew(),
            requesterId: requesterId,
            requesterEmail: requesterEmail,
            requesterPhoto: requesterPhoto,
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

#pragma warning disable CS8618
    private FriendshipRequest() {}
#pragma warning restore CS8618
}
