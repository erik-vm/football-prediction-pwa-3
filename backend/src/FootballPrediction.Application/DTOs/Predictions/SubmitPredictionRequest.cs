namespace FootballPrediction.Application.DTOs.Predictions;

public record SubmitPredictionRequest(Guid MatchId, int HomeScore, int AwayScore);
