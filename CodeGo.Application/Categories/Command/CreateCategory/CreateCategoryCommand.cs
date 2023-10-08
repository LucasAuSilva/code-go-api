
using CodeGo.Domain.CategoryAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Categories.Command.CreateCategory;

public record CreateCategoryCommand(
    string Name,
    string Description,
    int LanguageValue) : IRequest<ErrorOr<Category>>;
