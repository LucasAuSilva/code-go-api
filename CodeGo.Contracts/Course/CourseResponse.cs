
namespace CodeGo.Contracts.Course;

public record CourseResponse(
    string Id,
    string Name,
    string AuthorName,
    string Description,
    string? CourseIcon,
    Language Language,
    List<SectionResponse> Sections);

public record Language(
    string Name,
    int Value);

public record SectionResponse(
    string Id,
    string Name,
    string Description,
    List<ModuleResponse> Modules);

public record ModuleResponse(
    string Id,
    string Name,
    int TotalLessons,
    string ModuleType,
    int Difficulty);
