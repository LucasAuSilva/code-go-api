
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Application.Questions.Common;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Questions.Queries.ResolveQuestion;

public class ResolveQuestionQueryHandler : IRequestHandler<ResolveQuestionQuery, ErrorOr<ResolveQuestionResult>>
{
    private readonly IQuestionRepository _questionRepository;

    public ResolveQuestionQueryHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<ErrorOr<ResolveQuestionResult>> Handle(ResolveQuestionQuery query, CancellationToken cancellationToken)
    {
        var questionId = QuestionId.Create(query.QuestionId);
        var alternativeId = AlternativeId.Create(query.AlternativeId);

        var question = await _questionRepository.FindById(questionId);
        if (question is null)
            return Errors.Question.NotFound;
        var isCorrect = question.Resolve(alternativeId);
        if (isCorrect.IsError)
            return isCorrect.Errors;
        var message = isCorrect.Value ? "Resposta Correta" : "Resposta Errada";
        return new ResolveQuestionResult(
            message,
            isCorrect.Value);
    }
}
