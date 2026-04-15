namespace FootballPrediction.Application.DTOs.GameWeeks;

public record UpdateGameWeekRequest(int WeekNumber, string Name, DateTime StartDate, DateTime EndDate);
