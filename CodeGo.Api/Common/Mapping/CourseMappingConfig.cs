
using CodeGo.Application.Course.Command.CreateModule;
using CodeGo.Application.Course.Command.CreateSection;
using CodeGo.Contracts.Courses;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.Entities;
using CodeGo.Domain.ExerciseAggregateRoot;
using CodeGo.Domain.ExerciseAggregateRoot.Entities;
using CodeGo.Domain.QuestionAggregateRoot;
using CodeGo.Domain.QuestionAggregateRoot.Entity;
using Mapster;

namespace CodeGo.Api.Common.Mapping;

public class CourseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        CourseResponseMapping(config);
        CreateSectionMapping(config);
        CreateModuleMapping(config);
        PracticesResponseMapping(config);
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

    private static void PracticesResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Question, QuestionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString());

        config.NewConfig<Alternative, AlternativeResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<Exercise, ExerciseResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString());

        config.NewConfig<TestCase, TestCaseResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}
