
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.CourseAggregateRoot.ValueObjects;

public sealed class ModuleId : ValueObject
{
    public Guid Value { get; }

    private ModuleId(Guid value)
    {
        Value = value;
    }

    public static ModuleId CreateNew()
    {
        return new ModuleId(Guid.NewGuid());
    }

    public static ModuleId Create(string value)
    {
        return new ModuleId(Guid.Parse(value));
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
