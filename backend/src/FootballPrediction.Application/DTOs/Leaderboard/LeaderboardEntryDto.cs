namespace FootballPrediction.Application.DTOs.Leaderboard;

public record LeaderboardEntryDto(
    int Rank,
    Guid UserId,
    string Username,
    int TotalPoints,
    decimal TotalBonusPoints,
    int PredictionsCount,
    int ExactScores,
    int WinnerPlusDiff,
    int CorrectWinners,
    int OneScoreCorrect,
    int Misses
);
