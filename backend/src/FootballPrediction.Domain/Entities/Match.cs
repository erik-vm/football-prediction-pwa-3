using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Domain.Entities;

public class Match
{
    public Guid Id { get; set; }
    public Guid GameWeekId { get; set; }
    public string HomeTeam { get; set; } = string.Empty;
    public string AwayTeam { get; set; } = string.Empty;
    public string? HomeTeamCrest { get; set; }
    public string? AwayTeamCrest { get; set; }
    public DateTime KickoffTime { get; set; }
    public TournamentStage Stage { get; set; }
    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    public bool IsFinished { get; set; }
    public string? ExternalId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int StageMultiplier => Stage.GetMultiplier();

    public GameWeek? GameWeek { get; set; }
    public ICollection<Prediction>? Predictions { get; set; }
}
