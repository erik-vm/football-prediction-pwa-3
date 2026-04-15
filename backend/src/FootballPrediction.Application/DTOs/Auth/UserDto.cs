namespace FootballPrediction.Application.DTOs.Auth;

public record UserDto(Guid Id, string Username, string Email, string Role, DateTime CreatedAt);
