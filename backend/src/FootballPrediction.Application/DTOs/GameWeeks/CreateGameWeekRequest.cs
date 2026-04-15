namespace FootballPrediction.Application.DTOs.GameWeeks;

public record CreateGameWeekRequest(Guid TournamentId, int WeekNumber, string Name, DateTime StartDate, DateTime EndDate);
