
namespace CodeGo.Contracts.Users;

public record UserResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string? ProfilePicture,
    string? Bio,
    int StreakCount,
    int LifeCount,
    int LifeTotal,
    int ExperiencePoints,
    string Level,
    int Visibility,
    List<FriendshipRequestResponse> FriendshipRequests,
    List<string> FriendIds,
    List<string> CourseIds
);

public record FriendshipRequestResponse(
    string Id,
    string RequesterId,
    string RequesterEmail,
    string RequesterPhoto,
    string Message
);
