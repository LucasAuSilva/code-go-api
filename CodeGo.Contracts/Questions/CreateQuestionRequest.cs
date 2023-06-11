
namespace CodeGo.Contracts.Questions;

public record CreateQuestionRequest(
    string CourseId,
    string Title,
    string Description,
    string CategoryId,
    int DifficultyValue,
    List<Alternative> Alternatives
);

public record Alternative(
    string Description,
    bool IsCorrect
);
