
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Command.SendFriendshipRequest;

public class SendFriendshipRequestCommandHandler
    : IRequestHandler<SendFriendshipRequestCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public SendFriendshipRequestCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(
        SendFriendshipRequestCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var receiver = _userRepository.FindById(UserId.Create(command.ReceiverId));
        if (receiver is null)
            return Errors.User.NotFound;
        receiver.ReceiveFriendshipRequest(
            UserId.Create(command.UserId),
            command.Message);
        _userRepository.Update(receiver);
        return receiver;
    }
}
