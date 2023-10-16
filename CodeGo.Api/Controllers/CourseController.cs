
using CodeGo.Application.Courses.Command.CreateSection;
using CodeGo.Application.Courses.Command.CreateModule;
using CodeGo.Contracts.Courses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeGo.Application.Courses.Command.CreateCourse;
using CodeGo.Application.Courses.Queries.ListLanguages;
using CodeGo.Application.Courses.Queries.ListCourses;
using CodeGo.Application.Courses.Queries.FindCourse;

namespace CodeGo.Api.Controllers;

[Route("[controller]")]
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Languages()
    {
        var query = new LanguageQuery();
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<List<LanguageResponse>>(result)),
            Problem);
    }

    [HttpGet("{courseId}")]
    public async Task<IActionResult> FindCourse(string courseId)
    {
        var query = new FindCourseQuery(courseId);
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<CourseResponse>(result)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListCourses()
    {
        var query = new ListCoursesQuery();
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<List<CourseResponse>>(result)),
            Problem);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
    {
        var command = _mapper.Map<CreateCourseCommand>(request);
        var result = await _sender.Send(command);
        return result.Match(
            result => Created(_mapper.Map<CourseResponse>(result)),
            Problem);
    }

    [HttpPost("{courseId}/section")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateSection([FromBody] CreateSectionRequest request, string courseId)
    {
        var command = _mapper.Map<CreateSectionCommand>((request, courseId));
        var result = await _sender.Send(command);
        return result.Match(
            result => Created(_mapper.Map<CourseResponse>(result)),
            Problem);
    }

    [HttpPost("{courseId}/module")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateModule([FromBody] CreateModuleRequest request, string courseId)
    {
        var command = _mapper.Map<CreateModuleCommand>((request, courseId));
        var result = await _sender.Send(command);
        return result.Match(
            result => Created(_mapper.Map<CourseResponse>(result)),
            Problem);
    }
}
