using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Application.DTOs.Matches;

public record MatchDto(
    Guid Id,
    Guid GameWeekId,
    string GameWeekName,
    Guid TournamentId,
    string TournamentName,
    string HomeTeam,
    string AwayTeam,
    string? HomeTeamCrest,
    string? AwayTeamCrest,
    DateTime KickoffTime,
    TournamentStage Stage,
    string StageName,
    int StageMultiplier,
    int? HomeScore,
    int? AwayScore,
    bool IsFinished,
    string? ExternalId
);
