using FootballPrediction.Application.DTOs.Tournaments;

namespace FootballPrediction.Application.Interfaces.Services;

public interface ITournamentService
{
    Task<List<TournamentDto>> GetAllAsync();
    Task<TournamentDto> GetByIdAsync(Guid id);
    Task<TournamentDto> CreateAsync(CreateTournamentRequest request);
    Task<TournamentDto> UpdateAsync(Guid id, UpdateTournamentRequest request);
    Task DeleteAsync(Guid id);
    Task<TournamentDto?> GetActiveAsync();
}
