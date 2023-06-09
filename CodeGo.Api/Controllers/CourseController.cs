
using CodeGo.Application.Course.Command.CreateCourse;
using CodeGo.Application.Course.Common;
using CodeGo.Application.Course.Queries.ListLanguages;
using CodeGo.Application.Course.Queries.ListPractices;
using CodeGo.Contracts.Course;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            result => Ok(_mapper.Map<List<LanguageResult>>(result)),
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
