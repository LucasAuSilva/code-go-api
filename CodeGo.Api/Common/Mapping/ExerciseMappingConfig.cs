
using CodeGo.Contracts.Exercises;
using CodeGo.Domain.ExerciseAggregateRoot;
using Mapster;

using TestCase = CodeGo.Domain.ExerciseAggregateRoot.Entities.TestCase;

namespace CodeGo.Api.Common.Mapping;

public class ExerciseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Exercise, ExerciseResponse>()
            .Map(dest => dest.ExerciseId, src => src.Id.Value.ToString())
            .Map(dest => dest.DifficultyValue, src => src.Difficulty.Value)
            .Map(dest => dest.Type, src => src.Type.Name)
            .Map(dest => dest.CourseId, src => src.CourseId.Value.ToString())
            .Map(dest => dest.CategoryId, src => src.CategoryId.Value.ToString());

        config.NewConfig<TestCase, TestCaseResponse>()
            .Map(dest => dest.TestCaseId, src => src.Id.Value);
    }
}
