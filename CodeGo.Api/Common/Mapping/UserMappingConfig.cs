
using CodeGo.Contracts.Users;
using CodeGo.Domain.UserAggregateRoot;
using Mapster;

namespace CodeGo.Api.Common.Mapping;

public class UserMappingConfig
{
 public void Register(TypeAdapterConfig config)
    {
        UserResponseMapping(config);
    }

    private static void UserResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString());

        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.StreakCount, src => src.DayStreak.StreakCount);

        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.ExperiencePoints, src => src.Experience.Points);

        config.NewConfig<User, UserResponse>()
            .Map(
                dest => dest.CourseIds,
                src => src.CourseIds.AsEnumerable().Select(id => id.Value.ToString()).ToList());
    }
}
