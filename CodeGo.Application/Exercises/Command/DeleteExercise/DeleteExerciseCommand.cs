
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Exercises.Command.DeleteExercise;

public record DeleteExerciseCommand(
    string ExerciseId) : IRequest<ErrorOr<Deleted>>;
