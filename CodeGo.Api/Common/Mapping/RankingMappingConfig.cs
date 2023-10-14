
using CodeGo.Contracts.Rankings;
using CodeGo.Domain.RankingAggregateRoot;
using CodeGo.Domain.RankingAggregateRoot.Entities;
using Mapster;

namespace CodeGo.Api.Common.Mapping;

public class RankingMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        RankingResponseMapping(config);
    }

    private static void RankingResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Ranking, RankingResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString());
        config.NewConfig<RankingProgress, RankingProgressResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.Points, src => src.Points.Points)
            .Map(dest => dest.UserId, src => src.UserId.Value.ToString());
    }
}
