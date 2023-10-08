
using CodeGo.Api.Controllers;
using CodeGo.Application.Categories.Queries.ListAllCategories;
using CodeGo.Application.Common.Results;
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
    public async Task<IActionResult> GetAllCategories(
        [FromQuery] ListAllCategoriesRequest request)
    {
        var query = _mapper.Map<ListAllCategoriesQuery>(request);
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<PagedListResult<CategoryResponse>>(result)),
            Problem);
    }
}
