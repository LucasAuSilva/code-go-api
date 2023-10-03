
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
        var requesterId = UserId.Create(command.UserId);
        var requester = await _userRepository.FindById(requesterId)!;
        var receiver = await _userRepository.FindById(UserId.Create(command.ReceiverId));
        if (receiver is null || requester is null)
            return Errors.Users.NotFound;
        var result = receiver.ReceiveFriendshipRequest(
            requester,
            command.Message);
        if (result.IsError)
            return result.Errors;
        await _userRepository.Update(receiver);
        return result.Value;
    }
}
