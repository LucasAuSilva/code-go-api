
using CodeGo.Application.Questions.Queries.ResolveQuestion;
using CodeGo.Contracts.Questions;
using CodeGo.Domain.QuestionAggregateRoot;
using Mapster;

using Alternative = CodeGo.Domain.QuestionAggregateRoot.Entity.Alternative;

namespace CodeGo.Api.Common.Mapping;

public class QuestionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Question, QuestionResponse>()
            .Map(dest => dest.QuestionId, src => src.Id.Value.ToString())
            .Map(dest => dest.DifficultyValue, src => src.Difficulty.Value)
            .Map(dest => dest.CourseId, src => src.CourseId.Value.ToString())
            .Map(dest => dest.CategoryId, src => src.CategoryId.Value.ToString());

        config.NewConfig<Alternative, AlternativeResponse>()
            .Map(dest => dest.AlternativeId, src => src.Id.Value);

        config.NewConfig<(string QuestionId, string AlternativeId), ResolveQuestionQuery>()
            .Map(dest => dest.QuestionId, src => src.QuestionId)
            .Map(dest => dest.AlternativeId, src => src.AlternativeId);
    }
}
