
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CategoryAggregateRoot;
using CodeGo.Domain.Common.Enums;
using CodeGo.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Categories.Command.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ErrorOr<Category>>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Category>> Handle(
        CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        if (!Language.TryFromValue(command.LanguageValue, out var language))
            return Errors.Course.LanguageNotFound;
        var category = Category.CreateNew(
            command.Name,
            command.Description,
            language);
        await _categoryRepository.Add(category);
        return category;
    }
}
