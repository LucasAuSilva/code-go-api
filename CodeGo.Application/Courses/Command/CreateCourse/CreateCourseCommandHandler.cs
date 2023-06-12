
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot.Enums;
using CodeGo.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using CodeGo.Domain.CourseAggregateRoot;

namespace CodeGo.Application.Courses.Command.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, ErrorOr<Course>>
{
    private readonly ICourseRepository _courseRepository;

    public CreateCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<ErrorOr<Course>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var language = Language.FromValue(command.LanguageValue);
        if (language is null)
            return Errors.Course.LanguageNotFound;
        var course = Course.CreateNew(
            command.Name,
            command.Description,
            language);
        await _courseRepository.Add(course);
        return course;
    }
}
