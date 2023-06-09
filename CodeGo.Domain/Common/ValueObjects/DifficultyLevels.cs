
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.Common.ValueObjects;

public sealed class Difficulty : ValueObject
{
    public int Value { get; private set; }

    private Difficulty(int value)
    {
        Value = value;
    }

    public static Difficulty CreateNew(int difficulty)
    {
        return new Difficulty(difficulty);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static bool operator <=(Difficulty left, Difficulty right)
    {
        return left.Value <= right.Value;
    }

    public static bool operator >=(Difficulty left, Difficulty right)
    {
        return left.Value >= right.Value;
    }

#pragma warning disable CS8618
    private Difficulty() {}
#pragma warning restore CS8618
}
