
namespace CodeGo.Contracts.Users;

public record EditProfileRequest(
    string FirstName,
    string LastName,
    string Email,
    int Visibility, // can be 1 or 2
    string? Bio
);
