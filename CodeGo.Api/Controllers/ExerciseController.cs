
using CodeGo.Application.Exercises.Command.CreateExercise;
using CodeGo.Application.Exercises.Command.DeleteExercise;
using CodeGo.Application.Exercises.Command.EditExercise;
using CodeGo.Application.Exercises.Queries.ListExerciseByCourse;
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

    [HttpGet("{courseId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ListExerciseByCourse(string courseId)
    {
        var query = new ListExerciseByCourseQuery(courseId);
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<List<ExerciseResponse>>(result)),
            Problem);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateExercise([FromBody] CreateExerciseRequest request)
    {
        var command = _mapper.Map<CreateExerciseCommand>(request);
        var result = await _sender.Send(command);
        return result.Match(
            result => Created(_mapper.Map<ExerciseResponse>(result)),
            Problem);
    }

    [HttpPut("{exerciseId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EditExercise([FromBody] EditExerciseRequest request, string exerciseId)
    {
        var command = _mapper.Map<EditExerciseCommand>((request, exerciseId));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<ExerciseResponse>(result)),
            Problem);
    }

    [HttpDelete("{exerciseId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteExercise(string exerciseId)
    {
        var command = new DeleteExerciseCommand(exerciseId);
        var result = await _sender.Send(command);
        return result.Match(
            result => NoContent(),
            Problem);
    }
}
