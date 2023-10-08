
using ErrorOr;
using MediatR;
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.Entities;
using CodeGo.Domain.CourseAggregateRoot.Enums;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;

namespace CodeGo.Application.Courses.Command.CreateModule;

public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, ErrorOr<Course>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateModuleCommandHandler(
        ICourseRepository courseRepository,
        ICategoryRepository categoryRepository)
    {
        _courseRepository = courseRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Course>> Handle(CreateModuleCommand command, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.FindById(CourseId.Create(command.CourseId));
        if (course is null)
            return Errors.Course.CourseNotFound;
        var categoryId = CategoryId.Create(command.CategoryId);
        var category = await _categoryRepository.FindById(categoryId);
        if (category is null)
            return Errors.Categories.NotFound;
        var module = Module.CreateNew(
            command.Name,
            command.TotalLessons,
            ModuleType.FromValue(command.ModuleTypeValue),
            Difficulty.CreateNew(command.Difficulty),
            categoryId);
        course.AddModuleToSection(module, SectionId.Create(command.SectionId));
        await _courseRepository.Update(course);
        return course;
    }
}
