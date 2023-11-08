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
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.Entities;
using Mapster;

namespace CodeGo.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
 public void Register(TypeAdapterConfig config)
    {
        UserResponseMapping(config);
        RegisterCourseCommandMapping(config);
        SendFriendRequestMapping(config);
        UserProfileMapping(config);
        ResponseFriendshipRequestMapping(config);
        EditProfileRequestMapping(config);
        ListFriendsRequestsMapping(config);
        ListUsersByEmailRequestMapping(config);
        ListUsersByEmailResponseMapping(config);
        ListUsersByNameRequestMapping(config);
        ListUsersByNameResponseMapping(config);
        UpdateUserRoleRequestMapping(config);
        ListUserFriendsResponseMapping(config);
    }

    private static void ListUserFriendsResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<User, ListUserFriendsResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.Points, src => src.Points.Points)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.FirstName, src => src.FirstName);
    }

    private static void RegisterCourseCommandMapping(TypeAdapterConfig config)
    {
        config.NewConfig<(string UserId, string CourseId), RegisterCourseCommand>()
            .Map(dest => dest.CourseId, src => src.CourseId)
            .Map(dest => dest.UserId, src => src.UserId);
    }

    private static void UserResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.LifeCount, src => src.Life.Count)
            .Map(dest => dest.LifeTotal, src => src.Life.Total)
            .Map(dest => dest.StreakCount, src => src.DayStreak.StreakCount)
            .Map(dest => dest.ExperiencePoints, src => src.Points.Points)
            .Map(
                dest => dest.CourseIds,
                src => src.CourseIds.AsEnumerable().Select(id => id.Value.ToString()).ToList())
            .Map(
                dest => dest.FriendIds,
                src => src.FriendIds.AsEnumerable().Select(id => id.Value.ToString()).ToList())
            .Map(dest => dest.Visibility, src => src.Visibility.Value);
        
        config.NewConfig<FriendshipRequest, FriendshipRequestResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.RequesterId, src => src.RequesterId.Value.ToString());
    }

    private static void UserProfileMapping(TypeAdapterConfig config)
    {
        config.NewConfig<(string LoggedUserId, string UserId), UserProfileQuery>()
            .Map(dest => dest.LoggedUserId, src => src.LoggedUserId)
            .Map(dest => dest.UserId, src => src.UserId);
    }

    private static void SendFriendRequestMapping(TypeAdapterConfig config)
    {
        config.NewConfig<
            (string UserId, string ReceiverId, SendFriendshipRequest Request), SendFriendshipRequestCommand
        >()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.ReceiverId, src => src.ReceiverId)
            .Map(dest => dest, src => src.Request);
    }

    private static void EditProfileRequestMapping(TypeAdapterConfig config)
    {
        config.NewConfig<(
            string LoggedUserId, string UserId, EditProfileRequest Request), EditProfileCommand>()
            .Map(dest => dest.LoggedUserId , src => src.LoggedUserId)
            .Map(dest => dest.UserId , src => src.UserId)
            .Map(dest => dest, src => src.Request);
    }

    private static void ResponseFriendshipRequestMapping(TypeAdapterConfig config)
    {
        config.NewConfig<
            (string LoggedUserId, string UserId, string RequestId, ResponseFriendshipRequest Request), ResponseFriendshipRequestCommand
        >()
            .Map(dest => dest.LoggedUserId, src => src.LoggedUserId)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.RequestId, src => src.RequestId)
            .Map(dest => dest, src => src.Request);
    }

    private static void ListFriendsRequestsMapping(TypeAdapterConfig config)
    {
        config.NewConfig<(string LoggedUserId, string UserId, int Status), ListFriendsRequestsQuery>()
            .Map(dest => dest.LoggedUserId, src => src.LoggedUserId)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Status, src => src.Status);
    }

    private static void ListUsersByEmailRequestMapping(TypeAdapterConfig config)
    {
        config.NewConfig<ListUsersByEmailRequest, ListUsersByEmailQuery>()
            .Map(dest => dest.Email, src => src.Email);
    }

    private static void ListUsersByNameRequestMapping(TypeAdapterConfig config)
    {
        config.NewConfig<(ListUsersByNameRequest Request, string LoggedUserId), ListUsersByNameQuery>()
            .Map(dest => dest.LoggedUserId, src => src.LoggedUserId)
            .Map(dest => dest, src => src.Request);
    }

    private static void ListUsersByEmailResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<User, ListUsersByEmailResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.ProfilePicture, src => src.ProfilePicture)
            .Map(dest => dest.Role, src => src.Role.Name);
        config.NewConfig<PagedListResult<User>, PagedListResult<ListUsersByEmailResponse>>()
            .Fork(config => config.Default.PreserveReference(true))
            .Map(dest => dest.Data, src => src.Data.Adapt<List<ListUsersByEmailResponse>>())
            .PreserveReference(true);
    }

    private static void ListUsersByNameResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<User, ListUsersByNameResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.ProfilePicture, src => src.ProfilePicture)
            .Map(dest => dest.Visibility, src => src.Visibility.Value);
        config.NewConfig<PagedListResult<User>, PagedListResult<ListUsersByNameResponse>>()
            .Fork(config => config.Default.PreserveReference(true))
            .Map(dest => dest.Data, src => src.Data.Adapt<List<ListUsersByNameResponse>>())
            .PreserveReference(true);
    }

    private static void UpdateUserRoleRequestMapping(TypeAdapterConfig config)
    {
        config.NewConfig<(string UserId, int Role), UpdateUserRoleCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Role, src => src.Role);
    }
}
