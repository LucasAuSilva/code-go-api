
using CodeGo.Application.Questions.Common;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Questions.Query.ResolveQuestion;

public record ResolveQuestionQuery(
    string QuestionId,
    string AlternativeId) : IRequest<ErrorOr<ResolveQuestionResult>>;
