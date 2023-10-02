using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.Enums;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Command.ResponseFriendshipRequest;

public class ResponseFriendshipRequestCommandHandler
    : IRequestHandler<ResponseFriendshipRequestCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public ResponseFriendshipRequestCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(
        ResponseFriendshipRequestCommand command,
        CancellationToken cancellationToken)
    {
        if (!command.LoggedUserId.Equals(command.UserId))
            return Errors.Users.CantAccess;
        var userId = UserId.Create(command.UserId);
        var user = await _userRepository.FindById(userId);
        if (user is null)
            return Errors.Users.NotFound;
        var result = user.RespondFriendRequest(
            FriendshipRequestId.Create(command.RequestId),
            FriendshipRequestStatus.FromValue(command.Response));
        if (result.IsError)
            return result.Errors;
        var requester = await _userRepository.FindById(result.Value);
        if (requester is null)
            return Errors.Users.RequesterNotFound;
        requester.AddFriend(userId);
        await _userRepository.Update(user);
        return user;
    }
}
