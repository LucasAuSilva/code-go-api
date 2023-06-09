
using CodeGo.Contracts.Course;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.Entities;
using CodeGo.Domain.ExerciseAggregateRoot;
using CodeGo.Domain.ExerciseAggregateRoot.Entities;
using CodeGo.Domain.QuestionAggregateRoot.Entities;
using CodeGo.Domain.QuestionAggregateRoot.Entity;
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

        config.NewConfig<Question, QuestionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<Alternative, AlternativeResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<Exercise, ExerciseResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<TestCase, TestCaseResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}
