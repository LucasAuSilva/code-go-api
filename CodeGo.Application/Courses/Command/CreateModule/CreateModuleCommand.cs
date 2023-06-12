
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Courses.Command.CreateModule;

using CodeGo.Domain.CourseAggregateRoot;

public record CreateModuleCommand(
    string CourseId,
    string SectionId,
    string Name,
    int TotalLessons,
    int ModuleTypeValue,
    int Difficulty) : IRequest<ErrorOr<Course>>;
