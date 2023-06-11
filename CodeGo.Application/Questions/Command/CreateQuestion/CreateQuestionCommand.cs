
using CodeGo.Domain.QuestionAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Questions.Command.CreateQuestion;

public record CreateQuestionCommand(
    string Title,
    string Description,
    string CategoryId,
    string CourseId,
    int DifficultyValue,
    List<CreateAlternativeCommand> Alternatives) : IRequest<ErrorOr<Question>>;

public record CreateAlternativeCommand(
    string Description,
    bool IsCorrect
);
