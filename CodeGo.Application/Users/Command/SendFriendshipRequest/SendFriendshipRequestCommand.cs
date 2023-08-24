
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.Entities;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Command.SendFriendshipRequest;

public record SendFriendshipRequestCommand(
    string UserId,
    string ReceiverId,
    string? Message
) : IRequest<ErrorOr<User>>;
