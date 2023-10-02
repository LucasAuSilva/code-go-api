
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Command.RegisterCourse;

public class RegisterCourseCommandHandler : IRequestHandler<RegisterCourseCommand, ErrorOr<User>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;

    public RegisterCourseCommandHandler(ICourseRepository courseRepository, IUserRepository userRepository)
    {
        _courseRepository = courseRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(RegisterCourseCommand command, CancellationToken cancellationToken)
    {
        // Find If Course Exists
        var courseId = CourseId.Create(command.CourseId);
        var course = await _courseRepository.FindById(courseId);
        if (course is null)
            return Errors.Course.CourseNotFound;
        // Find User
        var user = await _userRepository.FindById(UserId.Create(command.UserId));
        if (user is null)
            return Errors.Users.NotFound;
        // Register Course In User
        user.RegisterCourse(courseId);
        // Save In Database
        await _userRepository.Update(user);
        // Return User
        return user;
    }
}
