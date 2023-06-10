
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot.Entities;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;

namespace CodeGo.Domain.ExerciseAggregateRoot;

public sealed class Exercise : AggregateRoot<ExerciseId, Guid>
{
    private List<TestCase> _testCases = new();
    public string Title { get; }
    public string Description { get; }
    public string BaseCode { get; }
    public Difficulty Difficulty { get; }
    public CategoryId CategoryId { get; }
    public CourseId CourseId { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public IReadOnlyCollection<TestCase> TestCases => _testCases;

    private Exercise(
        ExerciseId id,
        string title,
        string description,
        string baseCode,
        Difficulty difficulty,
        CategoryId categoryId,
        CourseId courseId,
        DateTime createdAt,
        DateTime updatedAt) : base(id)
    {
        Title = title;
        Description = description;
        BaseCode = baseCode;
        CategoryId = categoryId;
        Difficulty = difficulty;
        CourseId = courseId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Exercise CreateNew(
        string title,
        string description,
        string baseCode,
        Difficulty difficulty,
        CategoryId categoryId,
        CourseId courseId)
    {
        return new Exercise(
            id: ExerciseId.CreateNew(),
            title: title,
            description: description,
            baseCode: baseCode,
            difficulty: difficulty,
            categoryId: categoryId,
            courseId: courseId,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow);
    }

#pragma warning disable CS8618
    private Exercise() {}
#pragma warning restore CS8618
}
