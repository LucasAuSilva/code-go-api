
using System.Net;

namespace CodeGo.Contracts.Users;

public record ListUsersByEmailResponse(
    string Email,
    string ProfilePicture);
