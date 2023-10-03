
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.UserAggregateRoot.Entities;
using CodeGo.Domain.UserAggregateRoot.Enums;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Queries.ListFriendsRequests;

public class ListFriendsRequestsQueryHandler :
    IRequestHandler<ListFriendsRequestsQuery, ErrorOr<List<FriendshipRequest>>>
{
    private readonly IUserRepository _userRepository;

    public ListFriendsRequestsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<List<FriendshipRequest>>> Handle(
        ListFriendsRequestsQuery query,
        CancellationToken cancellationToken)
    {
        var userId = UserId.Create(query.UserId);
        var user = await _userRepository.FindById(userId);
        if (user is null)
            return Errors.Users.NotFound;
        if (!FriendshipRequestStatus.TryFromValue(query.Status, out var status))
            return Errors.Users.FriendRequestStatusIncorrect;
        return user.GetFriendRequests(status);
    }
}
