
using CodeGo.Application.Courses.Common;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Courses.Queries.ListPractices;

public record PracticesQuery(
    string CourseId,
    string ModuleId) : IRequest<ErrorOr<PracticesResult>>;
