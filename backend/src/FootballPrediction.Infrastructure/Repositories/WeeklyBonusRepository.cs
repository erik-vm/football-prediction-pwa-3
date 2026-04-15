using FootballPrediction.Application.Interfaces.Repositories;
using FootballPrediction.Domain.Entities;
using FootballPrediction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballPrediction.Infrastructure.Repositories;

public class WeeklyBonusRepository : IWeeklyBonusRepository
{
    private readonly AppDbContext _context;

    public WeeklyBonusRepository(AppDbContext context) => _context = context;

    public async Task<List<WeeklyBonus>> GetByGameWeekIdAsync(Guid gameWeekId) =>
        await _context.WeeklyBonuses.AsNoTracking()
            .Where(wb => wb.GameWeekId == gameWeekId)
            .ToListAsync();

    public async Task<List<WeeklyBonus>> GetByUserIdAsync(Guid userId) =>
        await _context.WeeklyBonuses.AsNoTracking()
            .Include(wb => wb.GameWeek)
            .Where(wb => wb.UserId == userId)
            .ToListAsync();

    public async Task<WeeklyBonus?> GetByUserAndGameWeekAsync(Guid userId, Guid gameWeekId) =>
        await _context.WeeklyBonuses.AsNoTracking()
            .FirstOrDefaultAsync(wb => wb.UserId == userId && wb.GameWeekId == gameWeekId);

    public async Task AddRangeAsync(IEnumerable<WeeklyBonus> bonuses)
    {
        foreach (var b in bonuses)
            b.Id = Guid.NewGuid();
        _context.WeeklyBonuses.AddRange(bonuses);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByGameWeekIdAsync(Guid gameWeekId)
    {
        var bonuses = await _context.WeeklyBonuses
            .Where(wb => wb.GameWeekId == gameWeekId)
            .ToListAsync();
        _context.WeeklyBonuses.RemoveRange(bonuses);
        await _context.SaveChangesAsync();
    }
}
