
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CategoryAggregateRoot;
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
        var categories = await _categoryRepository.ListAsync();
        return categories;
    }
}
