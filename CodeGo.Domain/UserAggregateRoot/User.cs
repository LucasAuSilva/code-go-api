
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using CodeGo.Domain.LevelAggregateRoot.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;

namespace CodeGo.Domain.UserAggregateRoot;

public sealed class User : AggregateRoot<UserId, Guid>
{
    private List<CourseId> _courseIds = new();
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string Password { get; }
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
            dayStreak: streak,
            experience: points,
            level: level,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow);
    }
}
