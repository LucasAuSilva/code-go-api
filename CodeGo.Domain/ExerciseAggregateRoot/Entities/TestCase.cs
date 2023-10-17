
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;

namespace CodeGo.Domain.ExerciseAggregateRoot.Entities;

public sealed class TestCase : Entity<TestCaseId>
{
    public string Title { get; private set; }
    public string Result { get; private set; }

    private TestCase(
        TestCaseId id,
        string title,
        string result) : base(id)
    {
        Title = title;
        Result = result;
    }

    public static TestCase CreateNew(
        string title,
        string result)
    {
        return new TestCase(
            TestCaseId.CreateNew(),
            title,
            result);
    }

    public static TestCase Create(
        TestCaseId testCaseId,
        string title,
        string result)
    {
        return new TestCase(
            testCaseId,
            title,
            result);
    }

#pragma warning disable CS8618
    private TestCase() {}
#pragma warning restore CS8618
}
