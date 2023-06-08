
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CouseAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Domain.CouseAggregateRoot.Entities;

public sealed class CourseProgress : Entity<CourseProgressId>
{
    private List<SectionId> _completedSectionIds = new();
    private List<ModuleId> _completedModuleIds = new();
    public UserId UserId { get; }
    public SectionId CurrentSectionId { get; }
    public ModuleId CurrentModuleId { get; }
    public Difficulty DifficultyLevel { get; }
    public IReadOnlyCollection<SectionId> CompletedSectionIds => _completedSectionIds;
    public IReadOnlyCollection<ModuleId> CompletedModuleIds => _completedModuleIds;

    private CourseProgress(
        CourseProgressId id,
        UserId userId,
        SectionId currentSectionId,
        ModuleId currentModuleId,
        Difficulty difficultyLevel) : base(id)
    {
        UserId = userId;
        CurrentSectionId = currentSectionId;
        CurrentModuleId = currentModuleId;
        DifficultyLevel = difficultyLevel;
    }

    public static CourseProgress CreateNew(
        UserId userId,
        SectionId currentSectionId,
        ModuleId currentModuleId,
        Difficulty difficultyLevel)
    {
        // TODO: think better in the difficulty level scale
        return new CourseProgress(
            CourseProgressId.CreateNew(),
            userId,
            currentSectionId,
            currentModuleId,
            difficultyLevel);
    }
}
