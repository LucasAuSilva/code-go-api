
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Command.UpdateUserRole;

public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserRoleCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(UpdateUserRoleCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindById(UserId.Create(command.UserId));
        if (user is null)
            return Errors.Users.NotFound;
        var result = user.ChangeRole(command.Role);
        if (result.IsError)
            return result.Errors;
        await _userRepository.Update(user);
        return user;
    }
}
