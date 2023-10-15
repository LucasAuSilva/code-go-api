
namespace CodeGo.Contracts.Progresses;

public record ProgressResponse(
    string Id,
    string UserId,
    string CourseId,
    string CurrentSectionId,
    List<ModuleTrackingResponse> ModuleTrackings,
    List<string> CompletedModuleIds,
    List<string> CompletedSectionIds);

public record ModuleTrackingResponse(
    string Id,
    string ModuleId,
    int LessonsCompleted,
    int Status);
