using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Application.Interfaces.Repositories;

public interface IGameWeekRepository
{
    Task<List<GameWeek>> GetByTournamentIdAsync(Guid tournamentId);
    Task<GameWeek?> GetByIdAsync(Guid id);
    Task<GameWeek> AddAsync(GameWeek gameWeek);
    Task<GameWeek> UpdateAsync(GameWeek gameWeek);
}
