using FootballPrediction.Application.Interfaces.Repositories;
using FootballPrediction.Domain.Entities;
using FootballPrediction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballPrediction.Infrastructure.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly AppDbContext _context;

    public MatchRepository(AppDbContext context) => _context = context;

    public async Task<List<Match>> GetUpcomingAsync(Guid? gameWeekId = null)
    {
        var query = _context.Matches
            .AsNoTracking()
            .Include(m => m.GameWeek)
                .ThenInclude(gw => gw!.Tournament)
            .AsQueryable();

        if (gameWeekId.HasValue)
            query = query.Where(m => m.GameWeekId == gameWeekId.Value);

        return await query.OrderBy(m => m.KickoffTime).ToListAsync();
    }

    public async Task<List<Match>> GetByGameWeekIdAsync(Guid gameWeekId) =>
        await _context.Matches.AsNoTracking()
            .Include(m => m.GameWeek)
                .ThenInclude(gw => gw!.Tournament)
            .Where(m => m.GameWeekId == gameWeekId)
            .OrderBy(m => m.KickoffTime)
            .ToListAsync();

    public async Task<Match?> GetByIdAsync(Guid id) =>
        await _context.Matches.AsNoTracking()
            .Include(m => m.GameWeek)
                .ThenInclude(gw => gw!.Tournament)
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task<Match?> GetByExternalIdAsync(string externalId) =>
        await _context.Matches.AsNoTracking()
            .FirstOrDefaultAsync(m => m.ExternalId == externalId);

    public async Task<Match> AddAsync(Match match)
    {
        match.Id = Guid.NewGuid();
        _context.Matches.Add(match);
        await _context.SaveChangesAsync();
        return match;
    }

    public async Task<Match> UpdateAsync(Match match)
    {
        _context.Matches.Update(match);
        await _context.SaveChangesAsync();
        return match;
    }

    public async Task DeleteByGameWeekIdAsync(Guid gameWeekId)
    {
        var matches = await _context.Matches.Where(m => m.GameWeekId == gameWeekId).ToListAsync();
        _context.Matches.RemoveRange(matches);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AllFinishedInGameWeekAsync(Guid gameWeekId) =>
        await _context.Matches
            .Where(m => m.GameWeekId == gameWeekId)
            .AllAsync(m => m.IsFinished);
}
