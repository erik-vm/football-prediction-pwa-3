using FootballPrediction.Application.Interfaces.Repositories;
using FootballPrediction.Domain.Entities;
using FootballPrediction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballPrediction.Infrastructure.Repositories;

public class PredictionRepository : IPredictionRepository
{
    private readonly AppDbContext _context;

    public PredictionRepository(AppDbContext context) => _context = context;

    public async Task<List<Prediction>> GetByUserIdAsync(Guid userId) =>
        await _context.Predictions.AsNoTracking()
            .Include(p => p.Match)
                .ThenInclude(m => m!.GameWeek)
                    .ThenInclude(gw => gw!.Tournament)
            .Include(p => p.User)
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.Match!.KickoffTime)
            .ToListAsync();

    public async Task<List<Prediction>> GetByMatchIdAsync(Guid matchId) =>
        await _context.Predictions.AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Match)
            .Where(p => p.MatchId == matchId)
            .ToListAsync();

    public async Task<Prediction?> GetByUserAndMatchAsync(Guid userId, Guid matchId) =>
        await _context.Predictions.AsNoTracking()
            .FirstOrDefaultAsync(p => p.UserId == userId && p.MatchId == matchId);

    public async Task<Prediction?> GetByIdAsync(Guid id) =>
        await _context.Predictions.AsNoTracking()
            .Include(p => p.Match)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<Prediction>> GetByGameWeekIdAsync(Guid gameWeekId) =>
        await _context.Predictions.AsNoTracking()
            .Include(p => p.Match)
            .Include(p => p.User)
            .Where(p => p.Match!.GameWeekId == gameWeekId)
            .ToListAsync();

    public async Task<Prediction> AddAsync(Prediction prediction)
    {
        prediction.Id = Guid.NewGuid();
        _context.Predictions.Add(prediction);
        await _context.SaveChangesAsync();
        return prediction;
    }

    public async Task<Prediction> UpdateAsync(Prediction prediction)
    {
        _context.Predictions.Update(prediction);
        await _context.SaveChangesAsync();
        return prediction;
    }
}
