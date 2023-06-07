
using MediatR;
using ErrorOr;
using CodeGo.Application.Authentication.Common;

namespace CodeGo.Application.Authentication.Command.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
