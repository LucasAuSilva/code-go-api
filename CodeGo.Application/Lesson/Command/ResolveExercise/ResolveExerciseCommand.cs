
using CodeGo.Application.Lesson.Common;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Lesson.Command.ResolveExercise;

public record ResolveExerciseCommand(
    string UserId,
    string LessonTrackingId,
    string ExerciseId,
    string TestCaseId,
    string SolutionCode) : IRequest<ErrorOr<ResolvePracticeResult>>;
