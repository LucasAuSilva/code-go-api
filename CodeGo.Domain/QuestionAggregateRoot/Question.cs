
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot.Entity;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;

namespace CodeGo.Domain.QuestionAggregateRoot;

public sealed class Question : AggregateRoot<QuestionId, Guid>
{
    public List<Alternative> _alternatives = new();
    public string Title { get; }
    public string Description { get; }
    public CategoryId CategoryId { get; }
    public Difficulty Difficulty { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public CourseId CourseId { get; }
    public IReadOnlyCollection<Alternative> Alternatives => _alternatives;

    private Question(
        QuestionId id,
        string title,
        string description,
        CategoryId categoryId,
        Difficulty difficulty,
        CourseId courseId,
        DateTime createdAt,
        DateTime updatedAt) : base(id)
    {
        Title = title;
        Description = description;
        Difficulty = difficulty;
        CategoryId = categoryId;
        CourseId = courseId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Question CreateNew(
        string title,
        string description,
        Difficulty difficulty,
        CategoryId categoryId,
        CourseId courseId)
    {
        return new Question(
            id: QuestionId.CreateNew(),
            title: title,
            description: description,
            categoryId: categoryId,
            difficulty: difficulty,
            courseId: courseId,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow);
    }

#pragma warning disable CS8618
    private Question() {}
#pragma warning restore CS8618
}
