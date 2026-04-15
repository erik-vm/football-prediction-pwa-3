namespace FootballPrediction.Application.DTOs.GameWeeks;

public record GameWeekDto(Guid Id, Guid TournamentId, int WeekNumber, string Name, DateTime StartDate, DateTime EndDate);
