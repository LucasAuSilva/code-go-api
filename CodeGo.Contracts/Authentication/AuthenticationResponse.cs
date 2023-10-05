
using CodeGo.Contracts.Users;

namespace CodeGo.Contracts.Authentication;

public record AuthenticationResponse(
    UserResponse User,
    string Token);
