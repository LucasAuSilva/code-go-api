
using CodeGo.Application.Rankings.Queries;
using CodeGo.Contracts.Rankings;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeGo.Api.Controllers;

[Route("[controller]")]
public class RankingController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public RankingController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetRankingDetails(string courseId)
    {
        var query = new RankingDetailsQuery(courseId);
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<RankingResponse>(result)),
            Problem);
    }
}
