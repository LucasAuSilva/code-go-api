
using CodeGo.Application.Questions.Command.CreateQuestion;
using CodeGo.Application.Questions.Command.DeleteQuestion;
using CodeGo.Application.Questions.Command.EditQuestion;
using CodeGo.Application.Questions.Queries.ListQuestionsByCourse;
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest request)
    {
        var command = _mapper.Map<CreateQuestionCommand>(request);
        var result = await _sender.Send(command);
        return result.Match(
            result => Created(_mapper.Map<QuestionResponse>(result)),
            Problem);
    }

    [HttpPut("{questionId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EditQuestion([FromBody] EditQuestionRequest request, string questionId)
    {
        var command = _mapper.Map<EditQuestionCommand>((request, questionId));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<QuestionResponse>(result)),
            Problem);
    }

    [HttpDelete("{questionId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteQuestion(string questionId)
    {
        var command = new DeleteQuestionCommand(questionId);
        var result = await _sender.Send(command);
        return result.Match(
            result => NoContent(),
            Problem);
    }


    [HttpGet("{courseId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ListQuestionByCourse(string courseId)
    {
        var query = new ListQuestionsByCourseQuery(courseId);
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<List<QuestionResponse>>(result)),
            Problem);
    }
}
