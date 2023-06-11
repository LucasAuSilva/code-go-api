
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

    public CreateQuestionCommandHandler(
        IQuestionRepository questionRepository,
        ICourseRepository courseRepository)
    {
        _questionRepository = questionRepository;
        _courseRepository = courseRepository;
    }

    public async Task<ErrorOr<Question>> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
    {
        var courseId = CourseId.Create(command.CourseId);
        var categoryId = CategoryId.Create(command.CategoryId);
        if (await _courseRepository.Exists(courseId))
            return Errors.Course.CourseNotFound;
        // TODO: After create category table, check if exists on database
        var difficulty = Difficulty.CreateNew(command.DifficultyValue);
        var question = Question.CreateNew(
            command.Title,
            command.Description,
            difficulty,
            categoryId,
            courseId,
            alternatives: command.Alternatives.ConvertAll(alternative => Alternative.CreateNew(
                alternative.Description,
                alternative.IsCorrect)));
        await _questionRepository.Add(question);
        return question;
    }
}
