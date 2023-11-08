
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Queries.ListUserFriends;

public class ListUserFriendsQueryHandler : IRequestHandler<ListUserFriendsQuery, ErrorOr<List<User>>>
{
    private readonly IUserRepository _userRepository;

    public ListUserFriendsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<List<User>>> Handle(ListUserFriendsQuery query, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(query.UserId);
        var users = await _userRepository.FindUserFriendsById(userId);
        return users
            .Where(user => user.FriendIds.Contains(userId))
            .ToList();
    }
}
