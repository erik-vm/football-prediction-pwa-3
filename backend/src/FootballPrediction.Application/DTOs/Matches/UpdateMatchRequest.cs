using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Application.DTOs.Matches;

public record UpdateMatchRequest(
    string HomeTeam,
    string AwayTeam,
    DateTime KickoffTime,
    TournamentStage Stage
);
