
using CodeGo.Domain.CourseAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Courses.Queries.ListCourses;

public record ListCoursesQuery() : IRequest<ErrorOr<List<Course>>>;
