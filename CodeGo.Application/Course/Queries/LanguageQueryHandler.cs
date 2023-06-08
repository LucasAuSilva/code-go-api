
using CodeGo.Application.Course.Common;
using CodeGo.Domain.CouseAggregateRoot.Enums;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Course.Queries;

public class LanguageQueryHandler : IRequestHandler<LanguageQuery, ErrorOr<List<LanguageResult>>>
{
    public async Task<ErrorOr<List<LanguageResult>>> Handle(LanguageQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        List<LanguageResult> languages = new();
        foreach (var option in Language.List)
        {
            languages.Add(new LanguageResult(option.Name, option.Value));
        }
        return languages;
    }
}
