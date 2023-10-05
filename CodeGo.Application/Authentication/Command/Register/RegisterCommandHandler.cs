
using CodeGo.Application.Authentication.Common;
using CodeGo.Domain.Common.Errors;
using CodeGo.Application.Common.Interfaces.Authentication;
using CodeGo.Application.Common.Interfaces.Persistance;
using ErrorOr;
using MediatR;
using CodeGo.Domain.UserAggregateRoot;

namespace CodeGo.Application.Authentication.Command.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHashGenerator _hashGenerator;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IHashGenerator hashGenerator,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _hashGenerator = hashGenerator;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // find email for validation
        var result = await _userRepository.FindByEmail(command.Email);
        if (result is not null)
            return Errors.Users.DuplicateEmail;
        // encrypt password
        var hashedPassword = _hashGenerator.GenerateHash(command.Password);
        // pass to domain
        var user = User.CreateNew(
            command.FirstName,
            command.LastName,
            command.Email,
            hashedPassword);
        // save user and return
        await _userRepository.Add(user);
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token);
    }
}
