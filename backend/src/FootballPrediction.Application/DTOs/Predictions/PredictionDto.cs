namespace FootballPrediction.Application.DTOs.Predictions;

public record PredictionDto(
    Guid Id,
    Guid UserId,
    string Username,
    Guid MatchId,
    string HomeTeam,
    string AwayTeam,
    DateTime KickoffTime,
    int PredictedHome,
    int PredictedAway,
    int? PointsEarned,
    int? ActualHome,
    int? ActualAway,
    bool MatchFinished,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
