namespace FootballPrediction.Domain.Entities;

public class GameWeek
{
    public Guid Id { get; set; }
    public Guid TournamentId { get; set; }
    public int WeekNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Tournament? Tournament { get; set; }
    public ICollection<Match>? Matches { get; set; }
    public ICollection<WeeklyBonus>? WeeklyBonuses { get; set; }
}
