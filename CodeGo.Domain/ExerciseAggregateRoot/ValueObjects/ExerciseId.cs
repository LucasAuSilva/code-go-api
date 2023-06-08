
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;

public sealed class ExerciseId : ValueObject
{
    public Guid Value { get; }

    private ExerciseId(Guid value)
    {
        Value = value;
    }

    public static ExerciseId CreateNew()
    {
        return new ExerciseId(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
