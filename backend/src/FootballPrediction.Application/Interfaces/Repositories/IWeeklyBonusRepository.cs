using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Application.Interfaces.Repositories;

public interface IWeeklyBonusRepository
{
    Task<List<WeeklyBonus>> GetByGameWeekIdAsync(Guid gameWeekId);
    Task<List<WeeklyBonus>> GetByUserIdAsync(Guid userId);
    Task<WeeklyBonus?> GetByUserAndGameWeekAsync(Guid userId, Guid gameWeekId);
    Task AddRangeAsync(IEnumerable<WeeklyBonus> bonuses);
    Task DeleteByGameWeekIdAsync(Guid gameWeekId);
}
