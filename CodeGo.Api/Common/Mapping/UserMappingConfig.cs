using CodeGo.Application.Users.Command.EditProfile;
using CodeGo.Application.Users.Command.RegisterCourse;
using CodeGo.Application.Users.Command.ResponseFriendshipRequest;
using CodeGo.Application.Users.Command.SendFriendshipRequest;
using CodeGo.Application.Users.Queries;
using CodeGo.Contracts.Users;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.Entities;
using Mapster;
using Microsoft.Extensions.Options;

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
            .Map(dest => dest.StreakCount, src => src.DayStreak.StreakCount)
            .Map(dest => dest.ExperiencePoints, src => src.Experience.Points)
            .Map(
                dest => dest.CourseIds,
                src => src.CourseIds.AsEnumerable().Select(id => id.Value.ToString()).ToList())
            .Map(
                dest => dest.FriendIds,
                src => src.FriendIds.AsEnumerable().Select(id => id.Value.ToString()).ToList())
            .Map(dest => dest.Visibility, src => src.Visibility.Value);
        
        config.NewConfig<FriendshipRequest, FriendshipRequestResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.RequesterId, src => src.Requester.Value.ToString());
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

}
