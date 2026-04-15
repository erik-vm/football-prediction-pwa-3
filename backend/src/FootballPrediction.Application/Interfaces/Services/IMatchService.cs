using FootballPrediction.Application.DTOs.Matches;

namespace FootballPrediction.Application.Interfaces.Services;

public interface IMatchService
{
    Task<List<MatchDto>> GetUpcomingAsync(Guid? gameWeekId = null);
    Task<List<MatchDto>> GetByGameWeekIdAsync(Guid gameWeekId);
    Task<MatchDto> GetByIdAsync(Guid id);
    Task<MatchDto> CreateAsync(CreateMatchRequest request);
    Task<MatchDto> UpdateAsync(Guid id, UpdateMatchRequest request);
    Task<MatchDto> EnterResultAsync(Guid id, EnterResultRequest request);
    Task<int> ImportFromApiAsync(Guid gameWeekId);
    Task DeleteByGameWeekIdAsync(Guid gameWeekId);
}
