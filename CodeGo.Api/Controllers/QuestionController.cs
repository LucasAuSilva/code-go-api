
using CodeGo.Application.Questions.Command.CreateQuestion;
using CodeGo.Application.Questions.Queries.ResolveQuestion;
using CodeGo.Contracts.Common;
using CodeGo.Contracts.Questions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeGo.Api.Controllers;

[Route("[controller]")]
public class QuestionController : ApiController
{
    private readonly IMapper _mapper;

    private readonly ISender _sender;

    public QuestionController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpPost]
    // [Authorize(Roles = "Admin")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest request)
    {
        var command = _mapper.Map<CreateQuestionCommand>(request);
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<QuestionResponse>(result)),
            Problem);
    }

    [HttpPost("{questionId}/resolve/{alternativeId}")]
    public async Task<IActionResult> ResolveQuestion(string questionId, string alternativeId)
    {
        var loggedUserId = GetUserId();
        if (loggedUserId is null)
            return Problem();
        var query = _mapper.Map<ResolveQuestionQuery>((questionId, loggedUserId, alternativeId));
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<ResolveResponse>(result)),
            Problem);
    }
}
