
namespace CodeGo.Contracts.Rankings;

public record RankingResponse(
    string Id,
    PeriodResponse Period,
    List<RankingProgressResponse> RankingProgresses
);

public record PeriodResponse(
    DateTime InitialDateTime,
    DateTime EndDateTime
);

public record RankingProgressResponse(
    string Id,
    string UserId,
    string UserFullName,
    int Points
);

    // public UserId UserId { get; private set; }
    // public string UserFullName { get; private set; }
    // public ExperiencePoints Points { get; private set; }
