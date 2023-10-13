
namespace CodeGo.Contracts.Questions;

public record EditQuestionRequest(
    string Title,
    string Description,
    string CategoryId,
    int DifficultyValue,
    List<EditAlternativeRequest> Alternatives
);

public record EditAlternativeRequest(
    string Id,
    string Description,
    bool IsCorrect
);
