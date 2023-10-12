
using CodeGo.Domain.ExerciseAggregateRoot;
using CodeGo.Domain.QuestionAggregateRoot;

namespace CodeGo.Application.Lesson.Common;

public record PracticesResult(
    List<Question> Questions,
    List<Exercise> Exercises);
