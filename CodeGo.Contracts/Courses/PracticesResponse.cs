
namespace CodeGo.Contracts.Courses;

public record PracticesResponse(
    List<QuestionResponse> Questions,
    List<ExerciseResponse> Exercises);

public record QuestionResponse(
    string Id,
    string Title,
    string Description,
    List<AlternativeResponse> Alternatives);

public record AlternativeResponse(
    string Id,
    string Description);

public record ExerciseResponse(
    string Id,
    string Title,
    string Description,
    string BaseCode,
    List<TestCaseResponse> TestCases);

public record TestCaseResponse(
    string Id,
    string Title);
