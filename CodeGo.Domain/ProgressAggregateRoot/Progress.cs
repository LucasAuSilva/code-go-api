
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
    private List<CategoryTracking> _categoriesTracking = new();
    private List<LessonTrackingId> _lessonTrackingIds = new();
    public UserId UserId { get; private set; }
    public CourseId CourseId { get; private set; }
    public ModuleId CurrentModule { get; private set; }
    public SectionId CurrentSection { get; private set; }
    public IReadOnlyCollection<CategoryTracking> CategoriesTracking => _categoriesTracking;
    public IReadOnlyCollection<ModuleId> CompletedModuleIds => _completedModuleIds;
    public IReadOnlyCollection<SectionId> CompletedSectionIds => _completedSectionIds;
    public IReadOnlyCollection<LessonTrackingId> LessonTrackingIds => _lessonTrackingIds;

    private Progress(
        ProgressId id,
        UserId userId,
        CourseId courseId,
        ModuleId currentModule,
        SectionId currentSection) : base(id)
    {
        UserId = userId;
        CourseId = courseId;
        CurrentModule = currentModule;
        CurrentSection = currentSection;
    }

    public static Progress CreateNew(
        UserId userId,
        CourseId courseId,
        ModuleId currentModule,
        SectionId currentSection)
    {
        return new Progress(
            ProgressId.CreateNew(),
            userId,
            courseId,
            currentModule,
            currentSection);
    }

#pragma warning disable CS8618
    private Progress() {}
#pragma warning restore CS8618
}
