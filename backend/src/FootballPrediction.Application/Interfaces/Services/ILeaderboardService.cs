using FootballPrediction.Application.DTOs.Leaderboard;

namespace FootballPrediction.Application.Interfaces.Services;

public interface ILeaderboardService
{
    Task<List<LeaderboardEntryDto>> GetOverallAsync();
    Task<List<WeeklyLeaderboardEntryDto>> GetWeeklyAsync(Guid gameWeekId);
    Task<UserStatsDto> GetUserStatsAsync(Guid userId);
    Task CalculateWeeklyBonusesAsync(Guid gameWeekId);
}
