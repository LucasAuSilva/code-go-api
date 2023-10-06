
namespace CodeGo.Contracts.Users;

public record ListUsersByEmailResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string ProfilePicture,
    string Role);
