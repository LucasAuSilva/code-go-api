
using CodeGo.Domain.ExerciseAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Exercises.Command.CreateExercise;

public record CreateExerciseCommand(
    string Title,
    string Description,
    string BaseCode,
    int DifficultyValue,
    int TypeValue,
    string CourseId,
    string CategoryId,
    List<CreateTestCaseCommand> TestCases) : IRequest<ErrorOr<Exercise>>;

public record CreateTestCaseCommand(
    string Title,
    string Result);
