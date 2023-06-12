
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Courses.Command.CreateModule;

using System.Threading;
using System.Threading.Tasks;
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.Entities;
using CodeGo.Domain.CourseAggregateRoot.Enums;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;

public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, ErrorOr<Course>>
{
    private readonly ICourseRepository _courseRepository;

    public CreateModuleCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<ErrorOr<Course>> Handle(CreateModuleCommand command, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.FindById(CourseId.Create(command.CourseId));
        if (course is null)
            return Errors.Course.CourseNotFound;
        var module = Module.CreateNew(
            command.Name,
            command.TotalLessons,
            ModuleType.FromValue(command.ModuleTypeValue),
            Difficulty.CreateNew(command.Difficulty));
        course.AddModuleToSection(module, SectionId.Create(command.SectionId));
        await _courseRepository.Update(course);
        return course;
    }
}
