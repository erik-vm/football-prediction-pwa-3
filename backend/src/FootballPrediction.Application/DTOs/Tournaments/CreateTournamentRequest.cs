namespace FootballPrediction.Application.DTOs.Tournaments;

public record CreateTournamentRequest(string Name, string Season, string? ExternalId, DateTime StartDate, DateTime EndDate, bool IsActive);
