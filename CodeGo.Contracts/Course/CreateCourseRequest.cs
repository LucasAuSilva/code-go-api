
namespace CodeGo.Contracts.Course;

public record CreateCourseRequest(
    string Name,
    string Description,
    int LanguageValue);
