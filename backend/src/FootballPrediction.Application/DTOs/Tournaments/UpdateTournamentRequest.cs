namespace FootballPrediction.Application.DTOs.Tournaments;

public record UpdateTournamentRequest(string Name, string Season, string? ExternalId, DateTime StartDate, DateTime EndDate, bool IsActive);
