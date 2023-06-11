
using CodeGo.Application.Questions.Command.CreateQuestion;
using CodeGo.Contracts.Questions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeGo.Api.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public class QuestionController : ApiController
{
    private readonly IMapper _mapper;

    private readonly ISender _sender;

    public QuestionController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest request)
    {
        var command = _mapper.Map<CreateQuestionCommand>(request);
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<QuestionResponse>(result)),
            Problem);
    }
}
