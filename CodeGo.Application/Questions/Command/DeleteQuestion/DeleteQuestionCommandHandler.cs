
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Questions.Command.DeleteQuestion;

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, ErrorOr<Deleted>>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ICourseRepository _courseRepository;

    public DeleteQuestionCommandHandler(
        IQuestionRepository questionRepository,
        ICourseRepository courseRepository)
    {
        _questionRepository = questionRepository;
        _courseRepository = courseRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteQuestionCommand command, CancellationToken cancellationToken)
    {
        var questionId = QuestionId.Create(command.QuestionId);
        var question = await _questionRepository.FindById(questionId);
        if (question is null)
            return Errors.Question.NotFound;
        var course = await _courseRepository.FindById(question.CourseId);
        course!.RemoveQuestionId(questionId);
        await _questionRepository.DeleteAsync(question);
        return Result.Deleted;
    }
}
