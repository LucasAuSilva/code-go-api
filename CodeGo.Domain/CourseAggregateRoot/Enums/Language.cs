
using Ardalis.SmartEnum;

namespace CodeGo.Domain.CourseAggregateRoot.Enums;

public sealed class Language : SmartEnum<Language>
{
    public static readonly Language Javascript = new(nameof(Javascript), 1);
    public static readonly Language Csharp = new(nameof(Csharp), 2);
    public static readonly Language Python = new(nameof(Python), 3);
    public static readonly Language Java = new(nameof(Java), 4);

    public Language(string name, int value) : base(name, value)
    {}
}
