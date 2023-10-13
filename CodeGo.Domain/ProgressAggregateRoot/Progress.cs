
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;
using CodeGo.Domain.ProgressAggregateRoot.Entities;
using CodeGo.Domain.ProgressAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Domain.ProgressAggregateRoot;

public sealed class Progress : AggregateRoot<ProgressId, Guid>
{
    private List<ModuleId> _completedModuleIds = new();
    private List<SectionId> _completedSectionIds = new();
    private List<CategoryTracking> _categoryTrackings = new();
    private List<LessonTrackingId> _lessonTrackingIds = new();
    private List<ModuleTracking> _moduleTrackings = new();
    public UserId UserId { get; private set; }
    public CourseId CourseId { get; private set; }
    public SectionId CurrentSection { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyCollection<CategoryTracking> CategoryTrackings => _categoryTrackings;
    public IReadOnlyCollection<ModuleTracking> ModuleTrackings => _moduleTrackings;
    public IReadOnlyCollection<ModuleId> CompletedModuleIds => _completedModuleIds;
    public IReadOnlyCollection<SectionId> CompletedSectionIds => _completedSectionIds;
    public IReadOnlyCollection<LessonTrackingId> LessonTrackingIds => _lessonTrackingIds;

    private Progress(
        ProgressId id,
        UserId userId,
        CourseId courseId,
        SectionId currentSection,
        DateTime createdAt,
        DateTime updatedAt) : base(id)
    {
        UserId = userId;
        CourseId = courseId;
        CurrentSection = currentSection;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Progress CreateNew(
        UserId userId,
        CourseId courseId,
        ModuleId currentModule,
        SectionId currentSection)
    {

        var progress = new Progress(
            ProgressId.CreateNew(),
            userId,
            courseId,
            currentSection,
            DateTime.UtcNow,
            DateTime.UtcNow);
        var moduleTracking = ModuleTracking.CreateNew(currentModule);
        progress.AddModuleTracking(moduleTracking);
        return progress;
    }

    public override ProgressId IdToValueObject()
    {
        return ProgressId.Create(Id.Value);
    }

    public void AddModuleTracking(ModuleTracking moduleTracking)
    {
        _moduleTrackings.Add(moduleTracking);
    }

    public void AddLessonTrackingId(LessonTrackingId lessonTrackingId)
    {
        _lessonTrackingIds.Add(lessonTrackingId);
    }

#pragma warning disable CS8618
    private Progress() {}
#pragma warning restore CS8618
}
