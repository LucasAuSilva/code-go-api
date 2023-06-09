
using CodeGo.Application.Course.Common;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Course.Queries.ListPractices;

public record PracticesQuery(
    string CourseId,
    string ModuleId) : IRequest<ErrorOr<PracticesResult>>;
