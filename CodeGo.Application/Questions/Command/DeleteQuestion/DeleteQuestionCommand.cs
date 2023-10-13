
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Questions.Command.DeleteQuestion;

public record DeleteQuestionCommand(
    string QuestionId) : IRequest<ErrorOr<Deleted>>;
