
using CodeGo.Contracts.Course;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.Entities;
using Mapster;

namespace CodeGo.Api.Common.Mapping;

public class CourseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Course, CourseResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<Section, SectionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<Module, ModuleResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}
