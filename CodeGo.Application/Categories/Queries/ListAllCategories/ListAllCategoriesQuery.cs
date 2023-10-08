
using CodeGo.Application.Common.Results;
using CodeGo.Domain.CategoryAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Categories.Queries.ListAllCategories;

public record ListAllCategoriesQuery(
    int Page,
    int PageSize) : IRequest<ErrorOr<PagedListResult<Category>>>;
