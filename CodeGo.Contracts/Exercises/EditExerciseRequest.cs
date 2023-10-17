
namespace CodeGo.Contracts.Exercises;

public record EditExerciseRequest(
    string Title,
    string Description,
    string BaseCode,
    int DifficultyValue,
    int TypeValue,
    string CategoryId,
    List<EditTestCaseRequest> TestCases);

public record EditTestCaseRequest(
    string TestCaseId,
    string Title,
    string Result);
