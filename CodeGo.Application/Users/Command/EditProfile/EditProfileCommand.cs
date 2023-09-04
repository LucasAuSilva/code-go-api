
using CodeGo.Domain.UserAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Command.EditProfile;

public record EditProfileCommand(
    string LoggedUserId,
    string UserId,
    string FirstName,
    string LastName,
    string Email,
    int Visibility, // can be 1 or 2
    string? Bio) : IRequest<ErrorOr<User>>;
