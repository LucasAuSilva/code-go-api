using CodeGo.Application.Users.Command.RegisterCourse;
using CodeGo.Application.Users.Command.ResponseFriendshipRequest;
using CodeGo.Application.Users.Command.SendFriendshipRequest;
using CodeGo.Application.Users.Queries;
using CodeGo.Contracts.Users;
using CodeGo.Domain.UserAggregateRoot.Entities;
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
        if (loggedUserId is null) return Problem();
        var query = _mapper.Map<UserProfileQuery>((loggedUserId, userId));
        var result = await _sender.Send(query); 
        return result.Match(
            result => Ok(_mapper.Map<UserResponse>(result)),
            Problem);
    }

    [HttpPost("{userId}/request/{receiverId}")]
    public async Task<IActionResult> SendFriendRequest(
        [FromBody] SendFriendshipRequest request,
        string userId,
        string receiverId)
    {
        var command = _mapper.Map<SendFriendshipRequestCommand>((userId, receiverId, request));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<FriendshipRequestResponse>(result)),
            Problem);
    }

    [HttpPost("{userId}/request/{requesterId}/response")]
    public async Task<IActionResult> ResponseFriendRequest(
        [FromBody] ResponseFriendshipRequest request,
        string userId,
        string requestId)
    {
        var command = _mapper.Map<ResponseFriendshipRequestCommand>((userId, requestId, request));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<UserResponse>(result)),
            Problem);
    }
}
