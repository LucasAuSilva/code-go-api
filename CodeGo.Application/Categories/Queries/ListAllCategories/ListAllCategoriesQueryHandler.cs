
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CategoryAggregateRoot;
using CodeGo.Domain.Common.Enums;
using CodeGo.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Categories.Queries.ListAllCategories;

public class ListAllCategoriesQueryHandler : IRequestHandler<ListAllCategoriesQuery, ErrorOr<List<Category>>>
{
    private readonly ICategoryRepository _categoryRepository;

    public ListAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<List<Category>>> Handle(
        ListAllCategoriesQuery query,
        CancellationToken cancellationToken)
    {
        if (!Language.TryFromValue(query.Language, out var language))
            return Errors.Categories.NotFound;
        var categories = await _categoryRepository.ListByLanguageAsync(language);
        return categories;
    }
}
