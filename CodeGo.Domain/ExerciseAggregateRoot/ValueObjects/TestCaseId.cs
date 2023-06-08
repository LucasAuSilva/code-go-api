
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;

public sealed class TestCaseId : ValueObject
{
    public Guid Value { get; }

    private TestCaseId(Guid value)
    {
        Value = value;
    }

    public static TestCaseId CreateNew()
    {
        return new TestCaseId(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
