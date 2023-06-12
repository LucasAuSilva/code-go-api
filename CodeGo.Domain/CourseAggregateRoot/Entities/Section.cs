
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;

namespace CodeGo.Domain.CourseAggregateRoot.Entities;

public sealed class Section : Entity<SectionId>
{
    private List<Module> _modules = new();
    public string Name { get; private set; }
    public string Description { get; private set; }
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

    public void AddModule(Module module)
    {
        _modules.Add(module);
    }

    public Module? GetModule(ModuleId moduleId)
    {
        return _modules.Find(module => module.Id == moduleId);
    }

    public bool HasModule(ModuleId moduleId)
    {
        var module = _modules.Find(module => module.Id == moduleId);
        return module != null;
    }
#pragma warning disable CS8618
    private Section() {}
#pragma warning restore CS8618
}
