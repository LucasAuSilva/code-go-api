
using CodeGo.Domain.UserAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Queries.UserProfile;

public record UserProfileQuery(
    string LoggedUserId,
    string UserId) : IRequest<ErrorOr<User>>;
