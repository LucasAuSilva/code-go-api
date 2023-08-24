using CodeGo.Application.Users.Command.RegisterCourse;
using CodeGo.Application.Users.Command.SendFriendshipRequest;
using CodeGo.Application.Users.Queries;
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
            .Map(dest => dest.Level, src => src.Level.Value.ToString())
            .Map(
                dest => dest.CourseIds,
                src => src.CourseIds.AsEnumerable().Select(id => id.Value.ToString()).ToList());
        
        config.NewConfig<FriendshipRequest, FriendshipRequestResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.RequesterId, src => src.Requester.ToString());
    }

    private static void UserProfile(TypeAdapterConfig config)
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

}
