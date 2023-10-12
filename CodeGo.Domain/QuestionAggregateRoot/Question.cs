
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot.Entity;
using CodeGo.Domain.QuestionAggregateRoot.Events;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;

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
        DateTime updatedAt,
        List<Alternative> alternatives) : base(id)
    {
        Title = title;
        Description = description;
        Difficulty = difficulty;
        CategoryId = categoryId;
        CourseId = courseId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _alternatives = alternatives;
    }

    public static ErrorOr<Question> CreateNew(
        string title,
        string description,
        Difficulty difficulty,
        CategoryId categoryId,
        CourseId courseId,
        List<Alternative>? alternatives = null)
    {
        if (alternatives is not null && alternatives.Any(alternative => alternative.IsCorrect))
            return Errors.Question.MissingCorrectAlternative;
        return new Question(
            id: QuestionId.CreateNew(),
            title: title,
            description: description,
            categoryId: categoryId,
            difficulty: difficulty,
            courseId: courseId,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow,
            alternatives: alternatives ?? new());
    }

    public ErrorOr<bool> Resolve(
        AlternativeId alternativeId,
        UserId userId)
    {
        var alternative = _alternatives.Find(a => a.Id == alternativeId);
        if (alternative is null)
            return Errors.Question.AlternativeNotFound;
        this.AddDomainEvent(
            new ResolvedQuestion(userId, this, alternative.IsCorrect));
        return alternative.IsCorrect;
    }

    public override QuestionId IdToValueObject()
    {
        return QuestionId.Create(Id.Value);
    }

#pragma warning disable CS8618
    private Question() {}
#pragma warning restore CS8618
}
