
using CodeGo.Domain.CourseAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Courses.Queries.FindCourse;

public record FindCourseQuery(
    string CourseId) : IRequest<ErrorOr<Course>>;
