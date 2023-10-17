
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot;
using CodeGo.Domain.ExerciseAggregateRoot.Entities;
using CodeGo.Domain.ExerciseAggregateRoot.Enums;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Exercises.Command.EditExercise;

public class EditExerciseCommandHandler : IRequestHandler<EditExerciseCommand, ErrorOr<Exercise>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public EditExerciseCommandHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<ErrorOr<Exercise>> Handle(
        EditExerciseCommand command,
        CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.FindById(
            ExerciseId.Create(command.ExerciseId));
        if (exercise is null)
            return Errors.Exercise.NotFound;
        exercise.Update(
            command.Title,
            command.Description,
            command.BaseCode,
            Difficulty.CreateNew(command.DifficultyValue),
            ExerciseType.FromValue(command.TypeValue),
            testCases: command.TestCases.ConvertAll(testCase => TestCase.Create(
                TestCaseId.Create(testCase.TestCaseId),
                testCase.Title,
                testCase.Result)));
        await _exerciseRepository.UpdateAsync(exercise);
        return exercise;
    }
}
