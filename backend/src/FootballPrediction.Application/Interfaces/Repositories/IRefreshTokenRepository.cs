using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<RefreshToken> AddAsync(RefreshToken token);
    Task<RefreshToken> UpdateAsync(RefreshToken token);
    Task RevokeAllForUserAsync(Guid userId);
}
