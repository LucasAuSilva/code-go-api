using CodeGo.Application.Users.Command;
using CodeGo.Application.Users.Queries;
using CodeGo.Contracts.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeGo.Api.Controllers;

[Route("[controller]")]
public class UserController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public UserController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpPost("{userId}/register/{courseId}")]
    public async Task<IActionResult> RegisterNewCourse(string userId, string courseId)
    {
        var command = _mapper.Map<RegisterCourseCommand>((userId, courseId));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<UserResponse>(result)),
            Problem);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> UserProfile(string userId)
    {
        var loggedUserId = GetUserId();
        var query = new UserProfileQuery(loggedUserId, userId);
        var result = await _sender.Send(query); 
        return result.Match(
            result => Ok(_mapper.Map<UserResponse>(result)),
            Problem);
    }
}
