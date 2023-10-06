
using CodeGo.Application.Exercises.Common;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Exercises.Queries.ResolveExercise;

public record ResolveExerciseQuery(
    string UserId,
    string ExerciseId,
    string TestCaseId,
    string SolutionCode) : IRequest<ErrorOr<ResolveExerciseResult>>;
