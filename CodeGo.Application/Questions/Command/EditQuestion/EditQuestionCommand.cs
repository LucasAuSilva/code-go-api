
using CodeGo.Domain.QuestionAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Questions.Command.EditQuestion;

public record EditQuestionCommand(
    string QuestionId,
    string Title,
    string Description,
    string CategoryId,
    int DifficultyValue,
    List<EditAlternativeCommand> Alternatives) : IRequest<ErrorOr<Question>>;

public record EditAlternativeCommand(
    string Id,
    string Description,
    bool IsCorrect
);
