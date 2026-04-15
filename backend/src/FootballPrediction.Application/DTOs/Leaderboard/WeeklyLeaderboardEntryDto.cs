namespace FootballPrediction.Application.DTOs.Leaderboard;

public record WeeklyLeaderboardEntryDto(
    int Rank,
    Guid UserId,
    string Username,
    int WeeklyPoints,
    decimal BonusPoints
);
