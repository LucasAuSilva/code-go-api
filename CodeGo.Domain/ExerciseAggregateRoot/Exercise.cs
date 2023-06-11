
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot.Entities;
using CodeGo.Domain.ExerciseAggregateRoot.Enums;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;

namespace CodeGo.Domain.ExerciseAggregateRoot;

public sealed class Exercise : AggregateRoot<ExerciseId, Guid>
{
    private List<TestCase> _testCases = new();
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string BaseCode { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public CourseId CourseId { get; private set; }
    public ExerciseType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyCollection<TestCase> TestCases => _testCases;

    private Exercise(
        ExerciseId id,
        string title,
        string description,
        string baseCode,
        Difficulty difficulty,
        ExerciseType type,
        CategoryId categoryId,
        CourseId courseId,
        List<TestCase> testCases,
        DateTime createdAt,
        DateTime updatedAt) : base(id)
    {
        Title = title;
        Description = description;
        BaseCode = baseCode;
        Type = type;
        CategoryId = categoryId;
        Difficulty = difficulty;
        CourseId = courseId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _testCases = testCases;
    }

    public static Exercise CreateNew(
        string title,
        string description,
        string baseCode,
        ExerciseType type,
        Difficulty difficulty,
        CategoryId categoryId,
        CourseId courseId,
        List<TestCase>? testCases = null)
    {
        return new Exercise(
            id: ExerciseId.CreateNew(),
            title: title,
            description: description,
            baseCode: baseCode,
            difficulty: difficulty,
            type: type,
            categoryId: categoryId,
            courseId: courseId,
            testCases: testCases ?? new(),
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow);
    }

#pragma warning disable CS8618
    private Exercise() {}
#pragma warning restore CS8618
}
