
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
    List<Alternative> Alternatives) : IRequest<ErrorOr<Question>>;

public record Alternative(
    string Description,
    bool IsCorrect
);
