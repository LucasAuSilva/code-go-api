
using CodeGo.Application.Lesson.Command.FinishLesson;
using CodeGo.Application.Lesson.Command.ResolveExercise;
using CodeGo.Application.Lesson.Command.ResolveQuestion;
using CodeGo.Application.Lesson.Command.StartLesson;
using CodeGo.Contracts.Common;
using CodeGo.Contracts.Exercises;
using CodeGo.Contracts.Lessons;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeGo.Api.Controllers;

[Route("[controller]")]
public class LessonController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public LessonController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("{courseId}/module/{moduleId}/start")]
    public async Task<IActionResult> StartLesson(
        string courseId,
        string moduleId)
    {
        var loggedUserId = GetUserId();
        if (loggedUserId is null)
            return Problem();
        var command = _mapper.Map<StartLessonCommand>((courseId, moduleId, loggedUserId));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(
                _mapper.Map<PracticesResponse>(result)),
                Problem);
    }

    [HttpPost("{lessonId}/resolve/question")]
    public async Task<IActionResult> ResolveQuestion(
        [FromBody] ResolveQuestionRequest request,
        string lessonId)
    {
        var loggedUserId = GetUserId();
        if (loggedUserId is null)
            return Problem();
        var command = _mapper.Map<ResolveQuestionCommand>((request, lessonId, loggedUserId));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(
                _mapper.Map<ResolveResponse>(result)),
                Problem);
    }

    [HttpPost("{lessonId}/resolve/exercise")]
    public async Task<IActionResult> ResolveExercise(
        [FromBody] ResolveExerciseRequest request,
        string lessonId
    )
    {
        var loggedUserId = GetUserId();
        if (loggedUserId is null)
            return Problem();
        var command = _mapper.Map<ResolveExerciseCommand>((request, lessonId, loggedUserId));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(
                _mapper.Map<ResolveResponse>(result)),
                Problem);
    }

    [HttpPost("{lessonId}/finish")]
    public async Task<IActionResult> FinishLesson(string lessonId)
    {
        var loggedUserId = GetUserId();
        if (loggedUserId is null)
            return Problem();
        var command = _mapper.Map<FinishLessonCommand>((lessonId, loggedUserId));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(
                _mapper.Map<FinishLessonResponse>(result)),
                Problem);
    }
}
