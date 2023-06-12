
namespace CodeGo.Contracts.Courses;

public record CreateCourseRequest(
    string Name,
    string Description,
    int LanguageValue);
