
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CouseAggregateRoot.ValueObjects;

namespace CodeGo.Domain.CouseAggregateRoot.Entities;

public sealed class Section : Entity<SectionId>
{
    private List<Module> _modules = new();
    public string Name { get; }
    public string Description { get; }
    public IReadOnlyCollection<Module> Modules => _modules;

    private Section(
        SectionId id,
        string name,
        string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static Section CreateNew(
        string name,
        string description)
    {
        return new Section(
            SectionId.CreateNew(),
            name,
            description);
    }
}
