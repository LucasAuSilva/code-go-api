
using CodeGo.Application.Lesson.Command.StartLesson;
using Mapster;
using CodeGo.Domain.ExerciseAggregateRoot;
using CodeGo.Domain.ExerciseAggregateRoot.Entities;
using CodeGo.Domain.QuestionAggregateRoot;
using CodeGo.Domain.QuestionAggregateRoot.Entity;
using CodeGo.Contracts.Lessons;
using CodeGo.Application.Lesson.Common;

namespace CodeGo.Api.Common.Mapping;

public class LessonMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        StartLessonRequestMapping(config);
    }

    private static void StartLessonRequestMapping(TypeAdapterConfig config)
    {
        config.NewConfig<
            (string CourseId, string ModuleId, string UserId), StartLessonCommand
        >()
            .Map(dest => dest.CourseId, src => src.CourseId)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.ModuleId, src => src.ModuleId);

    config.NewConfig<PracticesResult, PracticesResponse>()
            .Map(dest => dest.LessonId, src => src.LessonId.Value.ToString());

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
