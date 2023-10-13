
using CodeGo.Application.Lesson.Common;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Lesson.Command.FinishLesson;

public record FinishLessonCommand(
    string UserId,
    string LessonTrackingId) : IRequest<ErrorOr<FinishLessonResult>>;
