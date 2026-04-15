namespace FootballPrediction.Application.DTOs.Leaderboard;

public record UserStatsDto(
    Guid UserId,
    string Username,
    int OverallRank,
    int TotalPoints,
    decimal TotalBonusPoints,
    int PredictionsCount,
    int ExactScores,
    int WinnerPlusDiff,
    int CorrectWinners,
    int OneScoreCorrect,
    int Misses,
    List<WeeklyPointsDto> WeeklyHistory
);

public record WeeklyPointsDto(Guid GameWeekId, string GameWeekName, int Points, decimal BonusPoints);
