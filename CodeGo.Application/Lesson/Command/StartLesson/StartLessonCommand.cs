
using CodeGo.Application.Lesson.Common;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Lesson.Command.StartLesson;

public record StartLessonCommand(
    string CourseId,
    string ModuleId,
    string UserId) : IRequest<ErrorOr<PracticesResult>>;
