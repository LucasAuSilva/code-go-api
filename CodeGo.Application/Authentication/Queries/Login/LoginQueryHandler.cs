
using CodeGo.Domain.Common.Errors;
using CodeGo.Application.Authentication.Common;
using CodeGo.Application.Common.Interfaces.Authentication;
using CodeGo.Application.Common.Interfaces.Persistance;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHashComparer _hashComparer;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(
        IUserRepository userRepository,
        IHashComparer hashComparer,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _hashComparer = hashComparer;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // check if user exists
        var user = await _userRepository.FindByEmail(query.Email);
        if (user is null)
            return Errors.Authentication.InvalidCredentials;
        // check user hashed password
        if (_hashComparer.Compare(query.Password, user.Password))
            return Errors.Authentication.InvalidCredentials;
        // create token jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);
        // return result
        return new AuthenticationResult(
            user.Id.Value,
            user.FirstName,
            user.LastName,
            user.Email,
            token);
    }
}
