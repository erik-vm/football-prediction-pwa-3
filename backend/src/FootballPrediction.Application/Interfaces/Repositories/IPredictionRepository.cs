using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Application.Interfaces.Repositories;

public interface IPredictionRepository
{
    Task<List<Prediction>> GetByUserIdAsync(Guid userId);
    Task<List<Prediction>> GetByMatchIdAsync(Guid matchId);
    Task<Prediction?> GetByUserAndMatchAsync(Guid userId, Guid matchId);
    Task<Prediction?> GetByIdAsync(Guid id);
    Task<List<Prediction>> GetByGameWeekIdAsync(Guid gameWeekId);
    Task<Prediction> AddAsync(Prediction prediction);
    Task<Prediction> UpdateAsync(Prediction prediction);
}
