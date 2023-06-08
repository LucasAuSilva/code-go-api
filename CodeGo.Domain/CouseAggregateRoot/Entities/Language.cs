
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CouseAggregateRoot.ValueObjects;

namespace CodeGo.Domain.CouseAggregateRoot.Entities;

public sealed class Language : Entity<LanguageId>
{
    public string Name { get; }
    public string Description { get; }

    private Language(
        LanguageId id,
        string name,
        string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static Language CreateNew(
        string name,
        string description)
    {
        return new Language(
            LanguageId.CreateNew(),
            name,
            description);
    }
}
