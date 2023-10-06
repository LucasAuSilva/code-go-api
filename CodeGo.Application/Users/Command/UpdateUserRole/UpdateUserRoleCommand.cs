
using CodeGo.Domain.UserAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Command.UpdateUserRole;

public record UpdateUserRoleCommand(
    string UserId,
    int Role) : IRequest<ErrorOr<User>>;
