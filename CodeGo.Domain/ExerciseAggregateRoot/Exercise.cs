
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CouseAggregateRoot.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot.Entities;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;

namespace CodeGo.Domain.ExerciseAggregateRoot;

public sealed class Exercise : AggregateRoot<ExerciseId>
{
    private List<TestCase> _testCases = new();
    public string Title { get; }
    public string Description { get; }
    public string BaseCode { get; }
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
        CategoryId categoryId,
        CourseId courseId,
        DateTime createdAt,
        DateTime updatedAt) : base(id)
    {
        Title = title;
        Description = description;
        BaseCode = baseCode;
        CategoryId = categoryId;
        CourseId = courseId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Exercise CreateNew(
        string title,
        string description,
        string baseCode,
        CategoryId categoryId,
        CourseId courseId)
    {
        return new Exercise(
            id: ExerciseId.CreateNew(),
            title: title,
            description: description,
            baseCode: baseCode,
            categoryId: categoryId,
            courseId: courseId,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow);
    }
}
