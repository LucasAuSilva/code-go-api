
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.LessonTrackingAggregateRoot.Enums;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;
using CodeGo.Domain.ProgressAggregateRoot.Entities;
using CodeGo.Domain.ProgressAggregateRoot.Enums;
using CodeGo.Domain.ProgressAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;

namespace CodeGo.Domain.ProgressAggregateRoot;

public sealed class Progress : AggregateRoot<ProgressId, Guid>
{
    private List<ModuleId> _completedModuleIds = new();
    private List<SectionId> _completedSectionIds = new();
    private List<LessonTrackingId> _lessonTrackingIds = new();
    private List<ModuleTracking> _moduleTrackings = new();
    public UserId UserId { get; private set; }
    public CourseId CourseId { get; private set; }
    public SectionId CurrentSection { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
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

    // HACK: find better way o dealing with this rule
    public ErrorOr<Success> UpdateModuleTracking(Course course, LessonStatus status)
    {
        if (status.Equals(LessonStatus.Failed))
            return Result.Success;
        var currentModuleTracking = _moduleTrackings.FirstOrDefault(mt => mt.Status.Equals(ModuleStatus.Current));
        if (currentModuleTracking is null)
            return Errors.Progresses.NotFoundModuleTrackingWithCurrent;
        var resultModule = course.GetModuleFromId(currentModuleTracking.ModuleId);
        if (resultModule.IsError)
            return resultModule.Errors;
        currentModuleTracking.IncreaseLessonsCompleted(resultModule.Value.TotalLessons);
        if (currentModuleTracking.Status != ModuleStatus.Completed)
            return Result.Success;
        _completedModuleIds.Add(currentModuleTracking.ModuleId);
        var nextModuleId = course.UpdateProgress(this, currentModuleTracking.ModuleId);
        var moduleTracking = ModuleTracking.CreateNew(nextModuleId);
        _moduleTrackings.Add(moduleTracking);
        return Result.Success;
    }

    public void CompleteCurrentSection(SectionId sectionId)
    {
        _completedSectionIds.Add(CurrentSection);
        CurrentSection = sectionId;
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
