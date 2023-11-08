
using System.Net.WebSockets;
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot.Entities;
using CodeGo.Domain.ExerciseAggregateRoot.Enums;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;

namespace CodeGo.Domain.ExerciseAggregateRoot;

public sealed class Exercise : AggregateRoot<ExerciseId, Guid>
{
    private List<TestCase> _testCases = new();
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string BaseCode { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public CourseId CourseId { get; private set; }
    public ExerciseType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyCollection<TestCase> TestCases => _testCases;

    private Exercise(
        ExerciseId id,
        string title,
        string description,
        string baseCode,
        Difficulty difficulty,
        ExerciseType type,
        CategoryId categoryId,
        CourseId courseId,
        List<TestCase> testCases,
        DateTime createdAt,
        DateTime updatedAt) : base(id)
    {
        Title = title;
        Description = description;
        BaseCode = baseCode;
        Type = type;
        CategoryId = categoryId;
        Difficulty = difficulty;
        CourseId = courseId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _testCases = testCases;
    }

    public static ErrorOr<Exercise> CreateNew(
        string title,
        string description,
        string baseCode,
        ExerciseType type,
        Difficulty difficulty,
        CategoryId categoryId,
        CourseId courseId,
        List<TestCase>? testCases = null)
    {
        if (testCases is not null && testCases.Count >= 1)
            return Errors.Exercise.TestCaseNotFound;
        return new Exercise(
            id: ExerciseId.CreateNew(),
            title: title,
            description: description,
            baseCode: baseCode,
            difficulty: difficulty,
            type: type,
            categoryId: categoryId,
            courseId: courseId,
            testCases: testCases ?? new(),
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow);
    }

    public string MakeRunCode(string solutionCode)
    {
        if (Type == ExerciseType.FromZero)
            return solutionCode;
        return BaseCode.Replace("BaseCode", solutionCode);
    }

    public ErrorOr<bool> Resolve(
        UserId userId,
        TestCaseId testCaseId,
        string result)
    {
        var testCase = _testCases.Find(testCase => testCase.Id == testCaseId);
        if (testCase is null)
            return Errors.Exercise.TestCaseNotFound;
        var isCorrect = testCase.Result == result;
        return testCase.Result == result;
    }

    public override ExerciseId IdToValueObject()
    {
        return ExerciseId.Create(Id.Value);
    }

    public ErrorOr<Success> Update(
        string title,
        string description,
        string baseCode,
        Difficulty difficulty,
        ExerciseType type,
        List<TestCase> testCases)
    {
        ErrorOr<Success> result = Result.Success;
        foreach (var testCase in testCases)
        {
            if (_testCases.Exists(tc => tc.Id == testCase.Id))
                continue;
            result = Errors.Exercise.TestCaseNotFound;
            break;
        }
        Title = title;
        Description = description;
        BaseCode = baseCode;
        Difficulty = difficulty;
        Type = type;
        _testCases = testCases;
        return result;
    }

#pragma warning disable CS8618
    private Exercise() {}
#pragma warning restore CS8618
}
