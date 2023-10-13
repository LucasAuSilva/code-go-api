
using CodeGo.Domain.Common.Enums;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CourseAggregateRoot.Entities;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;
using CodeGo.Domain.ProgressAggregateRoot;
using CodeGo.Domain.QuestionAggregateRoot;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;
using ErrorOr;

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

    public int GetSectionPosition()
    {
        var lastSection = Sections
            .OrderBy(section => section.Position)
            .LastOrDefault();
        if (lastSection is not null)
            return lastSection.Position + 1;
        return 1;
    }

    public int GetModulePosition(SectionId sectionId)
    {
        var lastModule = Sections
            .FirstOrDefault(section => section.Id == sectionId)
            ?.Modules
                .OrderBy(module => module.Position)
                .LastOrDefault();
        if (lastModule is not null)
            return lastModule.Position + 1;
        return 1;
    }

    public ModuleId UpdateProgress(Progress progress, ModuleId currentModuleId)
    {
        var currentSection = _sections.Find(section => section.Id == progress.CurrentSection)!;
        var currentModule = currentSection.GetModule(currentModuleId)!;
        var currentSectionModulesOrdered = currentSection.Modules.OrderBy(module => module.Position);
        if (currentSectionModulesOrdered.Last().Id == currentModule.Id)
        {
            var sectionsOrdered = _sections.OrderBy(section => section.Position);
            var nextSection = sectionsOrdered.First(section => section.Position == currentSection.Position+1);
            progress.CompleteCurrentSection(nextSection.Id);
            return nextSection.Modules.OrderBy(module => module.Position).First().Id;
        }
        return currentSectionModulesOrdered
            .First(module => module.Position == currentModule.Position+1).Id;
    }

    public ErrorOr<Module> GetModuleFromId(ModuleId moduleId)
    {
        var module = Sections
            .FirstOrDefault(section => section.HasModule(moduleId))
            ?.GetModule(moduleId);
        if (module is null)
            return Errors.Course.ModuleNotFound;
        return module;
    }

    public ErrorOr<Success> AddModuleToSection(Module module, SectionId sectionId)
    {
        var section = _sections.Find(section => section.Id == sectionId)!;
        if (section.Modules.Any(m => m.Position.Equals(module.Position)))
            return Errors.Course.ModuleWithPositionAlreadyExists;
        section.AddModule(module);
        return Result.Success;
    }

    public ErrorOr<Success> AddSection(Section section)
    {
        if (Sections.Any(s => s.Position.Equals(section.Position)))
            return Errors.Course.SectionWithPositionAlreadyExists;
        _sections.Add(section);
        return Result.Success;
    }

    public bool HasModule(ModuleId moduleId)
    {
        var module = _sections
            .SelectMany(section => section.Modules)
            .Select(module => module)
            .Any(module => module.Id == moduleId);
        return module;
    }

    public SectionId FirstSection()
    {
        return Sections
            .Where(section => section.Position == 1)
            .Select(section => section.Id)
            .First();
    }

    public ModuleId FirstModule()
    {
        return Sections
            .OrderBy(section => section.Position)
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

    public override CourseId IdToValueObject()
    {
        return CourseId.Create(Id.Value);
    }

    public void AddQuestionId(QuestionId questionId)
    {
        _questionIds.Add(questionId)
    }

    public void AddExerciseId(ExerciseId exerciseId)
    {
        _exerciseIds.Add(exerciseId)
    }

#pragma warning disable CS8618
    private Course() {}
#pragma warning restore CS8618
}
