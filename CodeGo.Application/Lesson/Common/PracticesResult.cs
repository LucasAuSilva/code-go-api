
using CodeGo.Domain.ExerciseAggregateRoot;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot;

namespace CodeGo.Application.Lesson.Common;

public record PracticesResult(
    LessonTrackingId LessonId,
    List<Question> Questions,
    List<Exercise> Exercises);
