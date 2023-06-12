
using ErrorOr;
using MediatR;

using CourseAggregate = CodeGo.Domain.CourseAggregateRoot.Course;

namespace CodeGo.Application.Courses.Command.CreateCourse;

public record CreateCourseCommand(
    string Name,
    string Description,
    int LanguageValue) : IRequest<ErrorOr<CourseAggregate>>;
