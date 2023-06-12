
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;

public sealed class ExerciseId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private ExerciseId(Guid value)
    {
        Value = value;
    }

    public static ExerciseId CreateNew()
    {
        return new ExerciseId(Guid.NewGuid());
    }

    public static ExerciseId Create(Guid value)
    {
        return new ExerciseId(value);
    }

    public static ExerciseId Create(string value)
    {
        return new ExerciseId(Guid.Parse(value));
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
