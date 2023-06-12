
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Application.Exercises.Common;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using CodeGo.Application.Common.Interfaces.Http;

namespace CodeGo.Application.Exercises.Queries.ResolveExercise;

public class ResolveExerciseQueryHandler : IRequestHandler<ResolveExerciseQuery, ErrorOr<ResolveExerciseResult>>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ICompilerApi _compilerApi;

    public ResolveExerciseQueryHandler(
        IExerciseRepository exerciseRepository,
        ICompilerApi compilerApi)
    {
        _exerciseRepository = exerciseRepository;
        _compilerApi = compilerApi;
    }

    public async Task<ErrorOr<ResolveExerciseResult>> Handle(ResolveExerciseQuery query, CancellationToken cancellationToken)
    {
        var exerciseId = ExerciseId.Create(query.ExerciseId);
        var exercise = await _exerciseRepository.FindById(exerciseId);
        if (exercise is null)
            return Errors.Exercise.NotFound;
        var testCaseId = TestCaseId.Create(query.TestCaseId);
        var runCode = exercise.MakeRunCode(query.SolutionCode);
        var result = await _compilerApi.SendCodeToCompile(runCode);
        var isCorrect = exercise.Resolve(testCaseId, result);
        if (isCorrect.IsError)
            return isCorrect.Errors;
        var message = isCorrect.Value ? "Sucesso no Teste" : "Falha no Teste";
        return new ResolveExerciseResult(
            message,
            isCorrect.Value);
    }
}
