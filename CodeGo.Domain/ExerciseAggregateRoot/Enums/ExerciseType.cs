
using Ardalis.SmartEnum;

namespace CodeGo.Domain.ExerciseAggregateRoot.Enums;

public class ExerciseType : SmartEnum<ExerciseType>
{
    public static readonly ExerciseType Complete = new(nameof(Complete), 1);
    public static readonly ExerciseType FromZero = new(nameof(FromZero), 2);

    public ExerciseType(string name, int value) : base(name, value)
    {}
}
