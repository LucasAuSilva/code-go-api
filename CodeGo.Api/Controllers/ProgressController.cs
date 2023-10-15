
using CodeGo.Application.Progresses.Queries.GetProgressDetails;
using CodeGo.Contracts.Progresses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeGo.Api.Controllers;

[Route("[controller]")]
public class ProgressController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ProgressController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetProgressDetails(string courseId)
    {
        var loggedUserId = GetUserId();
        if (loggedUserId is null) return BadRequest();
        var query = new GetProgressDetailsQuery(loggedUserId, courseId);
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<ProgressResponse>(result)),
            Problem);
    }

}
