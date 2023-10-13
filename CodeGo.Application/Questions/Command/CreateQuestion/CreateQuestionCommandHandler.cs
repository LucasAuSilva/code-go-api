
using CodeGo.Domain.Common.Errors;
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.QuestionAggregateRoot;
using ErrorOr;
using MediatR;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot.Entity;

namespace CodeGo.Application.Questions.Command.CreateQuestion;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, ErrorOr<Question>>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateQuestionCommandHandler(
        IQuestionRepository questionRepository,
        ICourseRepository courseRepository,
        ICategoryRepository categoryRepository)
    {
        _questionRepository = questionRepository;
        _courseRepository = courseRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Question>> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
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
        var difficulty = Difficulty.CreateNew(command.DifficultyValue);
        var result = Question.CreateNew(
            command.Title,
            command.Description,
            difficulty,
            categoryId,
            courseId,
            alternatives: command.Alternatives.ConvertAll(alternative => Alternative.CreateNew(
                alternative.Description,
                alternative.IsCorrect)));
        if (result.IsError)
            return result.Errors;
        course.AddQuestionId(result.Value.IdToValueObject());
        await _questionRepository.Add(result.Value);
        return result.Value;
    }
}
