
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.ExerciseAggregateRoot;
using ErrorOr;
using MediatR;
using CodeGo.Domain.ExerciseAggregateRoot.Enums;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot.Entities;

namespace CodeGo.Application.Exercises.Command.CreateExercise;

public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, ErrorOr<Exercise>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IExerciseRepository _exerciseRepository;

    public CreateExerciseCommandHandler(
        IExerciseRepository exerciseRepository,
        ICourseRepository courseRepository)
    {
        _exerciseRepository = exerciseRepository;
        _courseRepository = courseRepository;
    }

    public async Task<ErrorOr<Exercise>> Handle(CreateExerciseCommand command, CancellationToken cancellationToken)
    {
        // TODO: Make validations for empty strings before command
        var courseId = CourseId.Create(command.CourseId);
        var categoryId = CategoryId.Create(command.CategoryId);
        if (!await _courseRepository.Exists(courseId))
            return Errors.Course.CourseNotFound;
        // TODO: when map the category create validation for check CategoryId
        var type = ExerciseType.FromValue(command.TypeValue);
        var difficulty = Difficulty.CreateNew(command.DifficultyValue);
        var exercise = Exercise.CreateNew(
            command.Title,
            command.Description,
            command.BaseCode,
            type,
            difficulty,
            categoryId,
            courseId,
            testCases: command.TestCases.ConvertAll(testCase => TestCase.CreateNew(
                testCase.Title,
                testCase.Result)));
        await _exerciseRepository.Add(exercise);
        return exercise;
    }
}
