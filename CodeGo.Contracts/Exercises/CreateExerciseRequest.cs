
namespace CodeGo.Contracts.Exercises;

public record CreateExerciseRequest(
    string Title,
    string Description,
    string BaseCode,
    int DifficultyValue,
    string CategoryId,
    string CourseId,
    List<TestCase> TestCases);

public record TestCase(
    string Title,
    string Result);
