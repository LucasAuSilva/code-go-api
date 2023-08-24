
using CodeGo.Domain.UserAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Command.RegisterCourse;

public record RegisterCourseCommand(
    string UserId,
    string CourseId) : IRequest<ErrorOr<User>>;
