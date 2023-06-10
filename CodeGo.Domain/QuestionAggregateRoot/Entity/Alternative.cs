
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;

namespace CodeGo.Domain.QuestionAggregateRoot.Entity;

public sealed class Alternative : Entity<AlternativeId>
{
    public string Description { get; }
    public bool IsCorrect { get; }

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

#pragma warning disable CS8618
    private Alternative() {}
#pragma warning restore CS8618
}
