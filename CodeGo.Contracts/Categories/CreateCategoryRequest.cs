
namespace CodeGo.Contracts.Categories;

public record CreateCategoryRequest(
    string Name,
    string Description,
    int LanguageValue);
