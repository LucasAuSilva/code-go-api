
using CodeGo.Domain.CategoryAggregateRoot;
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Enums;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface ICategoryRepository
{
    Task Add(Category category);
    Task<Category?> FindById(CategoryId categoryId);
    Task Update(Category category);
    Task<List<Category>> ListAsync();
    Task<List<Category>> ListByLanguageAsync(Language language);
}
