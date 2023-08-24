
using CodeGo.Domain.UserAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Command.ResponseFriendshipRequest;

public record ResponseFriendshipRequestCommand(
    string UserId,
    string RequesterId,
    int Response) : IRequest<ErrorOr<User>>;
