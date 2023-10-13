
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ProgressAggregateRoot.Enums;
using CodeGo.Domain.ProgressAggregateRoot.ValueObjects;

namespace CodeGo.Domain.ProgressAggregateRoot.Entities;

public sealed class ModuleTracking : Entity<ModuleTrackingId>
{
    public ModuleId ModuleId { get; private set; }
    public int LessonsCompleted { get; private set; }
    public ModuleStatus Status { get; private set; }

    private ModuleTracking(
        ModuleTrackingId id,
        ModuleId moduleId,
        int lessonsCompleted,
        ModuleStatus status) : base(id)
    {
        ModuleId = moduleId;
        LessonsCompleted = lessonsCompleted;
        Status = status;
    }

    public static ModuleTracking CreateNew(
        ModuleId moduleId)
    {
        return new ModuleTracking(
            ModuleTrackingId.CreateNew(),
            moduleId,
            0,
            ModuleStatus.Current);
    }

    public void IncreaseLessonsCompleted(int totalLessons)
    {
        LessonsCompleted++;
        var hasFinished = LessonsCompleted < totalLessons;
        Status = hasFinished ? Status : ModuleStatus.Completed;
    }

#pragma warning disable CS8618
    private ModuleTracking() {}
#pragma warning restore CS8618
}
