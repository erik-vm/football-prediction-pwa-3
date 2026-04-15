using FootballPrediction.Application.DTOs.GameWeeks;

namespace FootballPrediction.Application.Interfaces.Services;

public interface IGameWeekService
{
    Task<List<GameWeekDto>> GetByTournamentIdAsync(Guid tournamentId);
    Task<GameWeekDto> GetByIdAsync(Guid id);
    Task<GameWeekDto> CreateAsync(CreateGameWeekRequest request);
    Task<GameWeekDto> UpdateAsync(Guid id, UpdateGameWeekRequest request);
}
