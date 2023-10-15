using CodeGo.Application.Common.Results;
using CodeGo.Application.Users.Command.EditProfile;
using CodeGo.Application.Users.Command.RegisterCourse;
using CodeGo.Application.Users.Command.ResponseFriendshipRequest;
using CodeGo.Application.Users.Command.SendFriendshipRequest;
using CodeGo.Application.Users.Command.UpdateUserRole;
using CodeGo.Application.Users.Queries.ListFriendsRequests;
using CodeGo.Application.Users.Queries.ListUsersByEmail;
using CodeGo.Application.Users.Queries.ListUsersByName;
using CodeGo.Application.Users.Queries.UserProfile;
using CodeGo.Contracts.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("{userId}/requests")]
    public async Task<IActionResult> GetFriendRequests(string userId, int status = 1)
    {
        var loggedUserId = GetUserId();
        if (loggedUserId is null) return Problem();
        var query = _mapper.Map<ListFriendsRequestsQuery>((loggedUserId, userId, status));
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<List<FriendshipRequestResponse>>(result)),
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

    [HttpPost("{userId}/request/{requestId}/response")]
    public async Task<IActionResult> ResponseFriendRequest(
        [FromBody] ResponseFriendshipRequest request,
        string userId,
        string requestId)
    {
        var loggedUserId = GetUserId();
        if (loggedUserId is null) return Problem();
        var command = _mapper.Map<ResponseFriendshipRequestCommand>((loggedUserId ,userId, requestId, request));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<UserResponse>(result)),
            Problem);
    }

    // TODO: make ignore the logged user in the return of the users
    [HttpGet("list")]
    public async Task<IActionResult> FindUsersByName([FromQuery] ListUsersByNameRequest request)
    {
        var loggedUserId = GetUserId();
        if (loggedUserId is null) return Problem();
        var query = _mapper.Map<ListUsersByNameQuery>((loggedUserId, request));
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<PagedListResult<ListUsersByNameResponse>>(result)),
            Problem
        );
    }

    [HttpGet("admin/list")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> FindUsersByEmail([FromQuery] ListUsersByEmailRequest request)
    {
        var query = _mapper.Map<ListUsersByEmailQuery>(request);
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(_mapper.Map<PagedListResult<ListUsersByEmailResponse>>(result)),
            Problem
        );
    }

    [HttpPut("admin/{userId}/transform/{role}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUserRole(string userId, int role)
    {
        var command = _mapper.Map<UpdateUserRoleCommand>((userId, role));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<UserResponse>(result)),
            Problem
        );
    }

    [HttpPut("{userId}/edit")]
    public async Task<IActionResult> EditProfile(
        [FromBody] EditProfileRequest request,
        string userId)
    {
        var loggedUserId = GetUserId();
        if (loggedUserId is null) return Problem();
        var command = _mapper.Map<EditProfileCommand>((loggedUserId ,userId, request));
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(_mapper.Map<UserResponse>(result)),
            Problem);
    }
}
