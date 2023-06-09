
using Ardalis.SmartEnum;

namespace CodeGo.Domain.CourseAggregateRoot.Enums;

public sealed class ModuleType : SmartEnum<ModuleType>
{
    public static readonly Language Skill = new(nameof(Skill), 1);
    public static readonly Language Test = new(nameof(Test), 2);

    public ModuleType(string name, int value) : base(name, value)
    {}
}
