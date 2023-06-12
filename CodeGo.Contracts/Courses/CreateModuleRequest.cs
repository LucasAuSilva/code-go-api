
namespace CodeGo.Contracts.Courses;

public record CreateModuleRequest(
    string SectionId,
    string Name,
    int TotalLessons,
    int ModuleTypeValue,
    int Difficulty);
