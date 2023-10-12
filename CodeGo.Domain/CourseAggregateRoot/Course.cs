
using CodeGo.Domain.Common.Enums;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CourseAggregateRoot.Entities;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;

namespace CodeGo.Domain.CourseAggregateRoot;

public sealed class Course : AggregateRoot<CourseId, Guid>
{
    private List<ExerciseId> _exerciseIds = new();
    private List<QuestionId> _questionIds = new();
    private List<Section> _sections = new();
    public string Name { get; private set; }
    public string AuthorName { get; private set; }
    public string Description { get; private set; }
    public string? CourseIcon { get; private set; }
    public Language Language { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    // TODO: Section tem ordem de resolução colocar alguma lógica que defina isso
    public IReadOnlyCollection<Section> Sections => _sections;
    public IReadOnlyCollection<ExerciseId> ExerciseIds => _exerciseIds;
    public IReadOnlyCollection<QuestionId> QuestionIds => _questionIds;

    private Course(
        CourseId id,
        string name,
        string authorName,
        string description,
        string? courseIcon,
        Language language,
        DateTime createdAt,
        DateTime updatedAt) : base(id)
    {
        Name = name;
        AuthorName = authorName;
        Description = description;
        CourseIcon = courseIcon;
        Language = language;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Course CreateNew(
        string name,
        string description,
        Language language)
    {
        return new Course(
            id: CourseId.CreateNew(),
            name: name,
            authorName: "code&go",
            description: description,
            courseIcon: null,
            language: language,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow);
    }

    // TODO: Implementar invariantes nos métodos das classes
    public void AddModuleToSection(Module module, SectionId sectionId)
    {
        var section = _sections.Find(section => section.Id == sectionId)!;
        section.AddModule(module);
    }

    public void AddSection(Section section)
    {
        _sections.Add(section);
    }

    public bool HasModule(ModuleId moduleId)
    {
        var module = _sections
            .SelectMany(section => section.Modules)
            .Select(module => module)
            .ToList()
            .Find(module => module.Id == moduleId);
        return module != null;
    }

    // TODO: Implementar ordem para sections e modules
    public SectionId FirstSection()
    {
        return Sections.Select(section => section.Id).First();
    }

    public ModuleId FirstModule()
    {
        return Sections
            .First()
            .FirstModule();
    }

    public List<Question> SelectModuleQuestions(
        List<Question> questions,
        ModuleId moduleId)
    {
        var module = _sections
            .SelectMany(section => section.Modules)
            .Select(module => module)
            .ToList()
            .Find(module => module.Id == moduleId)!;
        var selectedQuestions = questions
            .Where(question =>
                question.Difficulty <= module.Difficulty)
            .Take(8)
            .ToList();
        return selectedQuestions.ToList();
    }

    public List<Exercise> SelectModuleExercises(
        List<Exercise> exercises,
        ModuleId moduleId)
    {
        var module = _sections
            .SelectMany(section => section.Modules)
            .Select(module => module)
            .ToList()
            .Find(module => module.Id == moduleId)!;
        var selectedExercises = exercises
            .Where(exercise =>
                exercise.Difficulty <= module.Difficulty)
            .Take(2)
            .ToList();
        return selectedExercises;
    }

#pragma warning disable CS8618
    private Course() {}
#pragma warning restore CS8618
}
