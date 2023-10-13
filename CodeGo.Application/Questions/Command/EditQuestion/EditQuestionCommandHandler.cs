
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot;
using CodeGo.Domain.QuestionAggregateRoot.Entity;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Questions.Command.EditQuestion;

public class EditQuestionCommandHandler : IRequestHandler<EditQuestionCommand, ErrorOr<Question>>
{
    private readonly IQuestionRepository _questionRepository;

    public EditQuestionCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<ErrorOr<Question>> Handle(
        EditQuestionCommand command,
        CancellationToken cancellationToken)
    {
        var question = await _questionRepository.FindById(QuestionId.Create(command.QuestionId));
        if (question is null)
            return Errors.Question.NotFound;
        var result = question.EditInfo(
            command.Title,
            command.Description,
            Difficulty.CreateNew(command.DifficultyValue),
            CategoryId.Create(command.CategoryId),
            alternatives: command.Alternatives.ConvertAll(alternative => Alternative.Create(
                AlternativeId.Create(alternative.Id),
                alternative.Description,
                alternative.IsCorrect)));
        if (result.IsError)
            return result.Errors;
        await _questionRepository.UpdateAsync(question);
        return question;
    }
}
