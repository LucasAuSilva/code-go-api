
namespace CodeGo.Contracts.Courses;

public record CreateSectionRequest(
    string Name,
    string Description,
    int Position);
