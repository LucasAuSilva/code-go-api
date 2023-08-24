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
        await Task.CompletedTask;
        var user = _userRepository.FindById(
            UserId.Create(command.UserId));
        if (user is null)
            return Errors.User.NotFound;
        user.RespondFriendRequest(
            FriendshipRequestId.Create(command.RequestId),
            FriendshipRequestStatus.FromValue(command.Response));
        _userRepository.Update(user);
        return user;
    }
}
