using CodeGo.Domain.Common.Models;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using CodeGo.Domain.LevelAggregateRoot.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.Enums;
using CodeGo.Domain.UserAggregateRoot.Entities;

namespace CodeGo.Domain.UserAggregateRoot;

public sealed class User : AggregateRoot<UserId, Guid>
{
    private List<CourseId> _courseIds = new();
    private List<UserId> _friendIds = new();
    private List<UserId> _blockedUserIds = new();
    private List<FriendshipRequest> _friendshipRequests = new();
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string Password { get; }
    public ProfileVisibility Visibility { get; }
    public UserRole Role { get; }
    public string? ProfilePicture { get; }
    public string? Bio { get; }
    public Streak DayStreak { get; }
    public ExperiencePoints Experience { get; }
    public LevelId Level { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public IReadOnlyCollection<CourseId> CourseIds => _courseIds;
    public IReadOnlyCollection<UserId> FriendIds => _friendIds;
    public IReadOnlyCollection<UserId> BlockedUserIds => _blockedUserIds;
    public IReadOnlyCollection<FriendshipRequest> FriendshipRequests => _friendshipRequests;

    private User(
        UserId id,
        string firstName,
        string lastName,
        string email,
        string password,
        ProfileVisibility visibility,
        UserRole role,
        Streak dayStreak,
        ExperiencePoints experience,
        LevelId level,
        DateTime createdAt,
        DateTime updatedAt,
        string? profilePicture = null,
        string? bio = null) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Visibility = visibility;
        Role = role;
        DayStreak = dayStreak;
        Experience = experience;
        Level = level;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        ProfilePicture = profilePicture;
        Bio = bio;
    }

    public static User CreateNew(
        string firstName,
        string lastName,
        string email,
        string password,
        LevelId level)
    {
        var streak = Streak.CreateNew();
        var points = ExperiencePoints.CreateNew();
        return new User(
            id: UserId.CreateNew(),
            firstName: firstName,
            lastName: lastName,
            email: email,
            password: password,
            visibility: ProfileVisibility.Private,
            role: UserRole.User,
            dayStreak: streak,
            experience: points,
            level: level,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow);
    }

    public void RegisterCourse(CourseId courseId)
    {
        _courseIds.Add(courseId);
    }

    public bool CheckProfileAccess(User accessUser)
    {
        if (Visibility == ProfileVisibility.Public)
            return true;
        if (accessUser.Role == UserRole.Admin)
            return true;
        if (_friendIds.Contains(accessUser.Id))
            return true;
        return false;
    }

    public void ReceiveFriendshipRequest(
        UserId userId,
        string? message
    )
    {
        // TODO: Invariant for checking if already has an friendship request with that user
        var friendshipRequest = FriendshipRequest.CreateNew(userId, message);
        _friendshipRequests.Add(friendshipRequest);
    }

    public bool RespondFriendRequest(
        UserId userId,
        FriendshipRequestStatus status
    )
    {
        var friendshipRequest = _friendshipRequests.First(fr => fr.Requester.Equals(userId));
        if (friendshipRequest is null)
            return false;
        status
            .When(FriendshipRequestStatus.Accepted).Then(() => {
                friendshipRequest.Accept();
                _friendIds.Add(userId);
            })
            .When(FriendshipRequestStatus.Blocked).Then(() => {
                friendshipRequest.Blocked();
                _blockedUserIds.Add(userId);
            })
            .When(FriendshipRequestStatus.Ignored).Then(friendshipRequest.Ignored)
            .When(FriendshipRequestStatus.Refused).Then(friendshipRequest.Refused);
        return true;
    }
}
