
using CodeGo.Application.Lesson.Common;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Lesson.Command.ResolveQuestion;

public record ResolveQuestionCommand(
    string UserId,
    string LessonTrackingId,
    string QuestionId,
    string AlternativeId
): IRequest<ErrorOr<ResolvePracticeResult>>;
