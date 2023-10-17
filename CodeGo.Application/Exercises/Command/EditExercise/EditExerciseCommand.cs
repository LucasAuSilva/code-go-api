
using CodeGo.Domain.ExerciseAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Exercises.Command.EditExercise;

public record EditExerciseCommand(
    string ExerciseId,
    string Title,
    string Description,
    string BaseCode,
    int DifficultyValue,
    int TypeValue,
    string CategoryId,
    List<EditTestCaseCommand> TestCases) : IRequest<ErrorOr<Exercise>>;

public record EditTestCaseCommand(
    string TestCaseId,
    string Title,
    string Result);
