
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CouseAggregateRoot.Entities;
using CodeGo.Domain.CouseAggregateRoot.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;

namespace CodeGo.Domain.CouseAggregateRoot;

public sealed class Course : AggregateRoot<CourseId>
{
    private List<ExerciseId> _exerciseIds = new();
    private List<QuestionId> _questionIds = new();
    private List<Section> _sections = new();
    private List<CourseProgress> _courseProgresses = new();
    public string Name { get; }
    public string AuthorName { get; }
    public string Description { get; }
    public string? CourseIcon { get; }
    public Language Language { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public IReadOnlyCollection<Section> Sections => _sections;
    public IReadOnlyCollection<ExerciseId> ExerciseIds => _exerciseIds;
    public IReadOnlyCollection<QuestionId> QuestionIds => _questionIds;
    public IReadOnlyCollection<CourseProgress> CourseProgresses => _courseProgresses;

    private Course(
        CourseId id,
        string name,
        string authorName,
        string description,
        string? courseIcon,
        Language language,
        DateTime createdAt,
        DateTime updatedAt) : base(id)
    {
        Name = name;
        AuthorName = authorName;
        Description = description;
        CourseIcon = courseIcon;
        Language = language;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Course CreateNew(
        string name,
        string description,
        Language language)
    {
        return new Course(
            id: CourseId.CreateNew(),
            name: name,
            authorName: "code&go",
            description: description,
            courseIcon: null,
            language: language,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow);
    }
}
