
using CodeGo.Domain.ExerciseAggregateRoot;
using CodeGo.Domain.QuestionAggregateRoot.Entities;

namespace CodeGo.Application.Course.Common;

public record PracticesResult(
    List<Question> Questions,
    List<Exercise> Exercises);

// public record QuestionResult(
//     string Id,
//     string Title,
//     string Description,
//     List<AlternativeResult> Alternatives);

// public record AlternativeResult(
//     string Id,
//     string Description);

// public record ExerciseResult(
//     string Id,
//     string Title,
//     string Description,
//     string BaseCode,
//     List<TestCaseResult> TestCases);

// public record TestCaseResult(
//     string Id,
//     string Title);
