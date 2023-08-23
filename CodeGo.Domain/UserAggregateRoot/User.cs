
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using CodeGo.Domain.LevelAggregateRoot.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.Enums;

namespace CodeGo.Domain.UserAggregateRoot;

public sealed class User : AggregateRoot<UserId, Guid>
{
    private List<CourseId> _courseIds = new();
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string Password { get; }
    public UserRole Role { get; }
    public string? ProfilePicture { get; }
    public string? Bio { get; }
    public Streak DayStreak { get; }
    public ExperiencePoints Experience { get; }
    public LevelId Level { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public IReadOnlyCollection<CourseId> CourseIds => _courseIds;

    private User(
        UserId id,
        string firstName,
        string lastName,
        string email,
        string password,
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
        // TODO: Make Check for user public profile
        if (accessUser.Role == UserRole.Admin)
            return true;
        // TODO: Make Check for user Friends can see
        return false;
    }
}
