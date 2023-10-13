
using CodeGo.Application.Courses.Command.CreateModule;
using CodeGo.Application.Courses.Command.CreateSection;
using CodeGo.Contracts.Courses;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.Entities;
using Mapster;

namespace CodeGo.Api.Common.Mapping;

public class CourseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        CourseResponseMapping(config);
        CreateSectionMapping(config);
        CreateModuleMapping(config);
    }

    private static void CreateModuleMapping(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateModuleRequest Request, string CourseId), CreateModuleCommand>()
            .Map(dest => dest.CourseId, src => src.CourseId)
            .Map(dest => dest, src => src.Request);
    }

    private static void CreateSectionMapping(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateSectionRequest Request, string CourseId), CreateSectionCommand>()
            .Map(dest => dest.CourseId, src => src.CourseId)
            .Map(dest => dest, src => src.Request);
    }

    private static void CourseResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Course, CourseResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString());

        config.NewConfig<Section, SectionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<Module, ModuleResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.ModuleType, src => src.Type.Name);
    }
}
