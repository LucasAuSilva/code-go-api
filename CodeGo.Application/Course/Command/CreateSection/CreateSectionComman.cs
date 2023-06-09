
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Course.Command.CreateSection;

using CodeGo.Domain.CourseAggregateRoot;

public record CreateSectionCommand(
    string CourseId,
    string Name,
    string Description) : IRequest<ErrorOr<Course>>;
