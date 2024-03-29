
using System.Configuration.Assemblies;
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Command.EditProfile;

public class EditProfileCommandHandler : IRequestHandler<EditProfileCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public EditProfileCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(EditProfileCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var user = await _userRepository.FindById(UserId.Create(command.UserId));
        if (user is null)
            return Errors.Users.NotFound;
        var result = user.EditProfile(
            firstName: command.FirstName,
            lastName: command.LastName,
            email: command.Email,
            visibility: command.Visibility,
            bio: command.Bio
        );
        if (result.IsError)
            return result.Errors;
        await _userRepository.Update(user);
        return user;
    }
}
