
using CodeGo.Application.Questions.Common;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Questions.Queries.ResolveQuestion;

public record ResolveQuestionQuery(
    string QuestionId,
    string UserId,
    string AlternativeId) : IRequest<ErrorOr<ResolveQuestionResult>>;
