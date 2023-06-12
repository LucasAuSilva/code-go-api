
using CodeGo.Application.Authentication.Common;
using CodeGo.Domain.Common.Errors;
using CodeGo.Application.Common.Interfaces.Authentication;
using CodeGo.Application.Common.Interfaces.Persistance;
using ErrorOr;
using MediatR;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.LevelAggregateRoot;
using CodeGo.Domain.LevelAggregateRoot.ValueObjects;

namespace CodeGo.Application.Authentication.Command.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILevelRepository _levelRepository;
    private readonly IHashGenerator _hashGenerator;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        ILevelRepository levelRepository,
        IHashGenerator hashGenerator,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _levelRepository = levelRepository;
        _hashGenerator = hashGenerator;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // find email for validation
        var result = _userRepository.FindByEmail(command.Email);
        if (result is not null)
            return Errors.User.DuplicateEmail;
        // encrypt password
        var hashedPassword = _hashGenerator.GenerateHash(command.Password);
        // level id
        var level = _levelRepository.FindWhenPreviousIsNull();
        if (level is null)
        {
            level = Level.CreateNew(1, 0);
            _levelRepository.Add(level);
        }
        // pass to domain
        var user = User.CreateNew(
            command.FirstName,
            command.LastName,
            command.Email,
            hashedPassword,
            LevelId.Create(level.Id.Value));
        // save user and return
        _userRepository.Add(user);
        var token = _jwtTokenGenerator.GenerateToken(user.Id.Value, user.FirstName, user.LastName);
        return new AuthenticationResult(
            user.Id.Value,
            user.FirstName,
            user.LastName,
            user.Email,
            token);
    }
}
