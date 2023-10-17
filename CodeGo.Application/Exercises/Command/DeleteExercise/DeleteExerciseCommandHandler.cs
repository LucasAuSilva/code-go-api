
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Exercises.Command.DeleteExercise;

public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, ErrorOr<Deleted>>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ICourseRepository _courseRepository;

    public DeleteExerciseCommandHandler(
        IExerciseRepository exerciseRepository,
        ICourseRepository courseRepository)
    {
        _exerciseRepository = exerciseRepository;
        _courseRepository = courseRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(
        DeleteExerciseCommand command,
        CancellationToken cancellationToken)
    {
        var exerciseId = ExerciseId.Create(command.ExerciseId);
        var exercise = await _exerciseRepository.FindById(exerciseId);
        if (exercise is null)
            return Errors.Exercise.NotFound;
        var course = await _courseRepository.FindById(exercise.CourseId);
        course!.RemoveExerciseId(exerciseId);
        await _exerciseRepository.DeleteAsync(exercise);
        return Result.Deleted;
    }
}
