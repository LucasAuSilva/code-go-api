
using CodeGo.Contracts.Progresses;
using CodeGo.Domain.ProgressAggregateRoot;
using CodeGo.Domain.ProgressAggregateRoot.Entities;
using Mapster;

namespace CodeGo.Api.Common.Mapping;

public class ProgressMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ProgressResponseMapping(config);
    }

    private static void ProgressResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Progress, ProgressResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.UserId, src => src.UserId.Value.ToString())
            .Map(dest => dest.CurrentSectionId, src => src.CurrentSection.Value.ToString())
            .Map(
                dest => dest.CompletedModuleIds,
                src => src.CompletedModuleIds.AsEnumerable().Select(id => id.Value.ToString()).ToList())
            .Map(
                dest => dest.CompletedSectionIds,
                src => src.CompletedSectionIds.AsEnumerable().Select(id => id.Value.ToString()).ToList());
        config.NewConfig<ModuleTracking, ModuleTrackingResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.ModuleId, src => src.ModuleId.Value.ToString())
            .Map(dest => dest.Status, src => src.Status.Value);
    }
}
