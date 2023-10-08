
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
    private readonly ICategoryRepository _categoryRepository;

    public CreateExerciseCommandHandler(
        IExerciseRepository exerciseRepository,
        ICourseRepository courseRepository,
        ICategoryRepository categoryRepository)
    {
        _exerciseRepository = exerciseRepository;
        _courseRepository = courseRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Exercise>> Handle(CreateExerciseCommand command, CancellationToken cancellationToken)
    {
        var courseId = CourseId.Create(command.CourseId);
        var categoryId = CategoryId.Create(command.CategoryId);
        var course = await _courseRepository.FindById(courseId);
        if (course is null)
            return Errors.Course.CourseNotFound;
        var category = await _categoryRepository.FindById(categoryId);
        if (category is null)
            return Errors.Categories.NotFound;
        if (!category.Language.Equals(course.Language))
            return Errors.Categories.NotEqualToCourse;
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
