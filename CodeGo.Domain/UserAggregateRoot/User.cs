using CodeGo.Domain.Common.Models;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.Enums;
using CodeGo.Domain.UserAggregateRoot.Entities;
using ErrorOr;
using CodeGo.Domain.Common.Errors;

namespace CodeGo.Domain.UserAggregateRoot;

public sealed class User : AggregateRoot<UserId, Guid>
{
    private List<CourseId> _courseIds = new();
    private List<UserId> _friendIds = new();
    private List<UserId> _blockedUserIds = new();
    private List<FriendshipRequest> _friendshipRequests = new();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; }
    public ProfileVisibility Visibility { get; private set; }
    public UserRole Role { get; }
    public string? ProfilePicture { get; }
    public string? Bio { get; private set; }
    public Streak DayStreak { get; }
    public ExperiencePoints Experience { get; }
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
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        ProfilePicture = profilePicture;
        Bio = bio;
    }

    public static User CreateNew(
        string firstName,
        string lastName,
        string email,
        string password)
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
        User user,
        string? message
    )
    {
        // TODO: Invariant for checking if already has an friendship request with that user
        // TODO: Understand if has to return something when user is blocked
        var userId = UserId.Create(user.Id.Value);
        if (_blockedUserIds.Contains(userId))
            return;
        var request = _friendshipRequests.Find(fr => fr.RequesterId.Equals(userId));
        if (request is not null && request.Status.Equals(FriendshipRequestStatus.Ignored))
            return;
        var friendshipRequest = FriendshipRequest.CreateNew(
            userId,
            user.Email,
            user.ProfilePicture,
            message);
        _friendshipRequests.Add(friendshipRequest);
    }

    public ErrorOr<UserId> RespondFriendRequest(
        FriendshipRequestId requestId,
        FriendshipRequestStatus status
    )
    {
        var request = _friendshipRequests.FirstOrDefault(fr => fr.Id.Equals(requestId));
        if (request is null)
            return Errors.Users.RequestNotFound;
        status
            .When(FriendshipRequestStatus.Accepted).Then(() => {
                request.Accept();
                AddFriend(request.RequesterId);
            })
            .When(FriendshipRequestStatus.Blocked).Then(() => {
                request.Blocked();
                _blockedUserIds.Add(request.RequesterId);
            })
            .When(FriendshipRequestStatus.Ignored).Then(request.Ignored)
            .When(FriendshipRequestStatus.Refused).Then(request.Refused);
        if (!request.Status.Equals(FriendshipRequestStatus.Accepted))
            return Error.Failure();
        return request.RequesterId;
    }

    public void AddFriend(UserId id)
    {
        _friendIds.Add(id);
    }

    public ErrorOr<Success> EditProfile(string firstName, string lastName, string email, int visibility, string? bio)
    {
        if (!ProfileVisibility.TryFromValue(visibility, out var profileVisibility))
            return Errors.Users.ProfileVisibilityIncorrect;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Visibility = profileVisibility;
        Bio = bio;
        return Result.Success;
    }

#pragma warning disable CS8618
    private User() {}
#pragma warning restore CS8618
}
