
using CodeGo.Domain.UserAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Queries.ListUserFriends;

public record ListUserFriendsQuery(
    string UserId) : IRequest<ErrorOr<List<User>>>;
