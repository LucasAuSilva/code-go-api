
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.CouseAggregateRoot.ValueObjects;

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

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
