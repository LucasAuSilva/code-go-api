
namespace CodeGo.Contracts.Questions;

public record QuestionResponse(
    string QuestionId,
    string Title,
    string Description,
    string CategoryId,
    int DifficultyValue,
    string CourseId,
    List<AlternativeResponse> Alternatives);

public record AlternativeResponse(
    string AlternativeId,
    string Description,
    bool IsCorrect);
