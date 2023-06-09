
namespace CodeGo.Contracts.Course;

public record CreateModuleRequest(
    string SectionId,
    string Name,
    int TotalLessons,
    int ModuleTypeValue,
    int Difficulty);
