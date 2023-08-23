
using System.Reflection.Metadata.Ecma335;
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Queries;

public class UserProfileQueryHandler : IRequestHandler<UserProfileQuery, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public UserProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(UserProfileQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Check if logged user exists
        var loggedUserId = UserId.Create(query.LoggedUserId);
        var loggedUser = _userRepository.FindById(loggedUserId);
        if (loggedUser is null)
            return Errors.User.UserNotFound;
        var userId = UserId.Create(query.UserId);
        if (loggedUser.Id.Equals(userId))
            return loggedUser;
        // Check if userId exists
        var user = _userRepository.FindById(userId);
        if (user is null)
            return Errors.User.UserNotFound;
        // Check if logged user can see profile
        if (!user.CheckProfileAccess(loggedUser))
            return Errors.User.CantAccess;
        // return profile
        return user;
    }
}
