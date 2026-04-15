namespace FootballPrediction.Domain.Entities;

public class WeeklyBonus
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid GameWeekId { get; set; }
    public decimal BonusPoints { get; set; }
    public int Rank { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
    public GameWeek? GameWeek { get; set; }
}
