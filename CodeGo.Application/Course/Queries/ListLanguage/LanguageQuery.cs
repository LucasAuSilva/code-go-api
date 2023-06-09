
using CodeGo.Application.Course.Common;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Course.Queries.ListLanguages;

public record LanguageQuery() : IRequest<ErrorOr<List<LanguageResult>>>;
