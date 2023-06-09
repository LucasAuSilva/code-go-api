
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot.Enums;
using CodeGo.Domain.Common.Errors;
using ErrorOr;
using MediatR;

using CourseAggregate = CodeGo.Domain.CourseAggregateRoot.Course;

namespace CodeGo.Application.Course.Command.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, ErrorOr<CourseAggregate>>
{
    private readonly ICourseRepository _courseRepository;

    public CreateCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<ErrorOr<CourseAggregate>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var language = Language.FromValue(command.LanguageValue);
        if (language is null)
            return Errors.Course.LanguageNotFound;
        var course = CourseAggregate.CreateNew(
            command.Name,
            command.Description,
            language);
        await _courseRepository.Add(course);
        return course;
    }
}
