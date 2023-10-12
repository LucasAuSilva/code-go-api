
namespace CodeGo.Contracts.Lessons;

public record ResolveExerciseRequest(
    string ExerciseId,
    string TestCaseId,
    string SolutionCode);
