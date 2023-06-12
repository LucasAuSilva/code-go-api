
using CodeGo.Domain.CourseAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Courses.Command.CreateCourse;

public record CreateCourseCommand(
    string Name,
    string Description,
    int LanguageValue) : IRequest<ErrorOr<Course>>;
