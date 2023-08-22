
using CodeGo.Application.Users.Command;
using CodeGo.Contracts.Users;
using CodeGo.Domain.UserAggregateRoot;
using Mapster;

namespace CodeGo.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
 public void Register(TypeAdapterConfig config)
    {
        UserResponseMapping(config);
        RegisterCourseCommandMapping(config);
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
    }

}
