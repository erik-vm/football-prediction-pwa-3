using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Application.Interfaces.Repositories;

public interface IMatchRepository
{
    Task<List<Match>> GetUpcomingAsync(Guid? gameWeekId = null);
    Task<List<Match>> GetByGameWeekIdAsync(Guid gameWeekId);
    Task<Match?> GetByIdAsync(Guid id);
    Task<Match?> GetByExternalIdAsync(string externalId);
    Task<Match> AddAsync(Match match);
    Task<Match> UpdateAsync(Match match);
    Task<bool> AllFinishedInGameWeekAsync(Guid gameWeekId);
}
