
namespace CodeGo.Contracts.Authentication;

public record AuthenticationResponse(
    Guid UserId,
    string FirstName,
    string Email,
    string Token);
