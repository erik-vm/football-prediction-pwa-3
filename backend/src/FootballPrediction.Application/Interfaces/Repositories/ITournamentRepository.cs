using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Application.Interfaces.Repositories;

public interface ITournamentRepository
{
    Task<List<Tournament>> GetAllAsync();
    Task<Tournament?> GetByIdAsync(Guid id);
    Task<Tournament?> GetActiveAsync();
    Task<Tournament> AddAsync(Tournament tournament);
    Task<Tournament> UpdateAsync(Tournament tournament);
    Task DeleteAsync(Tournament tournament);
}
