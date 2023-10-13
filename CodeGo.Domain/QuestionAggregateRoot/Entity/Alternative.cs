
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;

namespace CodeGo.Domain.QuestionAggregateRoot.Entity;

public sealed class Alternative : Entity<AlternativeId>
{
    public string Description { get; private set; }
    public bool IsCorrect { get; private set; }

    private Alternative(
        AlternativeId id,
        string description,
        bool isCorrect) : base (id)
    {
        Description = description;
        IsCorrect = isCorrect;
    }

    public static Alternative CreateNew(
        string description,
        bool isCorrect)
    {
        return new Alternative(
            AlternativeId.CreateNew(),
            description,
            isCorrect);
    }

    public static Alternative Create(
        AlternativeId id,
        string description,
        bool isCorrect)
    {
        return new Alternative(
            id,
            description,
            isCorrect);
    }

#pragma warning disable CS8618
    private Alternative() {}
#pragma warning restore CS8618
}
