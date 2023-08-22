
namespace CodeGo.Contracts.Users;

public record UserResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string? ProfilePicture,
    string? Bio,
    int StreakCount,
    int ExperiencePoints,
    string Level,
    List<string> CourseIds
);
