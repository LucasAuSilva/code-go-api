
using CodeGo.Application.Course.Command.CreateSection;
using CodeGo.Application.Course.Command.CreateModule;
using CodeGo.Contracts.Courses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeGo.Application.Course.Command.CreateCourse;
using CodeGo.Application.Course.Queries.ListLanguages;
using CodeGo.Application.Course.Queries.ListPractices;

namespace CodeGo.Api.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public class CourseController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CourseController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet("languages")]
    public async Task<IActionResult> Languages()
    {
        var query = new LanguageQuery();
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<List<LanguageResponse>>(result)),
            Problem);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
    {
        var command = _mapper.Map<CreateCourseCommand>(request);
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<CourseResponse>(result)),
            Problem);
    }

    [HttpPost("{courseId}/section")]
    public async Task<IActionResult> CreateSection([FromBody] CreateSectionRequest request, string courseId)
    {
        var command = _mapper.Map<CreateSectionCommand>((request, courseId));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<CourseResponse>(result)),
            Problem);
    }

    [HttpPost("{courseId}/module")]
    public async Task<IActionResult> CreateModule([FromBody] CreateModuleRequest request, string courseId)
    {
        var command = _mapper.Map<CreateModuleCommand>((request, courseId));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<CourseResponse>(result)),
            Problem);
    }

    [HttpPost("{courseId}/module/{moduleId}/start")]
    public async Task<IActionResult> GetModulePractices(
        string courseId,
        string moduleId)
    {
        var query = new PracticesQuery(
            courseId,
            moduleId);
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(
                _mapper.Map<PracticesResponse>(result)),
                Problem);
    }
}
