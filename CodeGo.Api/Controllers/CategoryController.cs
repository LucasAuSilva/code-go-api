
using CodeGo.Application.Categories.Command.CreateCategory;
using CodeGo.Application.Categories.Queries.ListAllCategories;
using CodeGo.Contracts.Categories;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeGo.Api.Controllers;

[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class CategoryController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CategoryController(
        IMapper mapper,
        ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var query = new ListAllCategoriesQuery();
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<List<CategoryResponse>>(result)),
            Problem);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var command = _mapper.Map<CreateCategoryCommand>(request);
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<CategoryResponse>(result)),
            Problem);
    }
}
