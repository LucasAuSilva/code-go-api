
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
    public string Title { get; private set; }
    public string Description { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public CourseId CourseId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
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
