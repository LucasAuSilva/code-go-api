
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.UserAggregateRoot.Entities;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Command.SendFriendshipRequest;

public class SendFriendshipRequestCommandHandler
    : IRequestHandler<SendFriendshipRequestCommand, ErrorOr<FriendshipRequest>>
{
    private readonly IUserRepository _userRepository;

    public SendFriendshipRequestCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<FriendshipRequest>> Handle(
        SendFriendshipRequestCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var requesterId = UserId.Create(command.UserId);
        var requester = _userRepository.FindById(requesterId)!;
        var receiver = _userRepository.FindById(UserId.Create(command.ReceiverId));
        if (receiver is null)
            return Errors.User.NotFound;
        receiver.ReceiveFriendshipRequest(
            requester,
            command.Message);
        _userRepository.Update(receiver);
        var request = receiver.FriendshipRequests.FirstOrDefault(
            fr => fr.RequesterId.Equals(requesterId)
        );
        if (request is null)
            return Errors.User.NotFound;
        return request;
    }
}
