using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Application.DTOs.Matches;

public record ExternalMatchDto(
    string ExternalId,
    string HomeTeam,
    string AwayTeam,
    string? HomeTeamCrest,
    string? AwayTeamCrest,
    DateTime KickoffTime,
    TournamentStage Stage,
    int? HomeScore,
    int? AwayScore,
    bool IsFinished
);
