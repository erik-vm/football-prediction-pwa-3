using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Application.DTOs.Matches;

public record CreateMatchRequest(
    Guid GameWeekId,
    string HomeTeam,
    string AwayTeam,
    DateTime KickoffTime,
    TournamentStage Stage
);
