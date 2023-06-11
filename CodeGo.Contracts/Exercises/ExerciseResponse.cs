
namespace CodeGo.Contracts.Exercises;

public record ExerciseResponse(
    string ExerciseId,
    string Title,
    string Description,
    int Difficulty,
    string Type,
    string BaseCode,
    string CourseId,
    string CategoryId,
    List<TestCaseResponse> TestCases
);

public record TestCaseResponse(
    string Title,
    string Result);
