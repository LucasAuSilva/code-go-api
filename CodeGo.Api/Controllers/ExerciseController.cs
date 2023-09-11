
using CodeGo.Application.Exercises.Command.CreateExercise;
using CodeGo.Application.Exercises.Queries.ResolveExercise;
using CodeGo.Contracts.Common;
using CodeGo.Contracts.Exercises;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeGo.Api.Controllers;

[Route("[controller]")]
public class ExerciseController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public ExerciseController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpPost]
    // [Authorize(Roles = "Admin")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateExercise([FromBody] CreateExerciseRequest request)
    {
        var command = _mapper.Map<CreateExerciseCommand>(request);
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<ExerciseResponse>(result)),
            Problem);
    }

    [HttpPost("{exerciseId}/resolve/{testCaseId}")]
    public async Task<IActionResult> ResolveExercise([FromBody] ResolveExerciseRequest request, string exerciseId, string testCaseId)
    {
        var query = _mapper.Map<ResolveExerciseQuery>((exerciseId, testCaseId, request));
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<ResolveResponse>(result)),
            Problem);
    }
}
