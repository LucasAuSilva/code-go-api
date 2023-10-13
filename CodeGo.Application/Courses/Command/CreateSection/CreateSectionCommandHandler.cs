
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Courses.Command.CreateSection;

using System.Threading;
using System.Threading.Tasks;
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.Entities;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;

public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, ErrorOr<Course>>
{
    private readonly ICourseRepository _courseRepository;

    public CreateSectionCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<ErrorOr<Course>> Handle(CreateSectionCommand command, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.FindById(CourseId.Create(command.CourseId));
        if (course is null)
            return Errors.Course.CourseNotFound;
        var sectionPosition = course.GetSectionPosition();
        var section = Section.CreateNew(
            command.Name,
            command.Description,
            sectionPosition);
        var result = course.AddSection(section);
        if (result.IsError)
            return result.Errors;
        await _courseRepository.Update(course);
        return course;
    }
}
