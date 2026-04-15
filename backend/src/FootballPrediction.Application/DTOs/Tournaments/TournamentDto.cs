namespace FootballPrediction.Application.DTOs.Tournaments;

public record TournamentDto(Guid Id, string Name, string Season, string? ExternalId, DateTime StartDate, DateTime EndDate, bool IsActive, DateTime CreatedAt);
