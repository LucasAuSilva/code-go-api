
using CodeGo.Application.Courses.Common;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Courses.Queries.ListLanguages;

public record LanguageQuery() : IRequest<ErrorOr<List<LanguageResult>>>;
