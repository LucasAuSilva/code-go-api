
using CodeGo.Application.Exercises.Command.CreateExercise;
using CodeGo.Contracts.Exercises;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeGo.Api.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public class ExerciseController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public ExerciseController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public async Task<IActionResult> CreateExercise([FromBody] CreateExerciseRequest request)
    {
        var command = _mapper.Map<CreateExerciseCommand>(request);
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<ExerciseResponse>(result)),
            Problem);
    }
}