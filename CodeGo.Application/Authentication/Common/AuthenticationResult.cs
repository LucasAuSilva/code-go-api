
namespace CodeGo.Application.Authentication.Common;

public record AuthenticationResult(
    Guid UserId,
    string FirstName,
    string Email,
    string Token);
