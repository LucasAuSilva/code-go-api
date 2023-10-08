
using CodeGo.Domain.CourseAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Courses.Command.CreateModule;


public record CreateModuleCommand(
    string CourseId,
    string SectionId,
    string Name,
    string CategoryId,
    int TotalLessons,
    int ModuleTypeValue,
    int Difficulty) : IRequest<ErrorOr<Course>>;
