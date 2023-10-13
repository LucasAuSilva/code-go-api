
namespace CodeGo.Contracts.Courses;

public record CreateModuleRequest(
    string SectionId,
    string Name,
    string CategoryId,
    int TotalLessons,
    int Position,
    int ModuleTypeValue,
    int Difficulty);
