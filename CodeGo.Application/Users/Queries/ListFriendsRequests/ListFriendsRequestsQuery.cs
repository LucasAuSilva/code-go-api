
using CodeGo.Domain.UserAggregateRoot.Entities;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Queries.ListFriendsRequests;

public record ListFriendsRequestsQuery(
    string LoggedUserId,
    string UserId,
    int Status
) : IRequest<ErrorOr<List<FriendshipRequest>>>;
