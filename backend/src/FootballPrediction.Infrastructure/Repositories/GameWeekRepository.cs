using FootballPrediction.Application.Interfaces.Repositories;
using FootballPrediction.Domain.Entities;
using FootballPrediction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballPrediction.Infrastructure.Repositories;

public class GameWeekRepository : IGameWeekRepository
{
    private readonly AppDbContext _context;

    public GameWeekRepository(AppDbContext context) => _context = context;

    public async Task<List<GameWeek>> GetByTournamentIdAsync(Guid tournamentId) =>
        await _context.GameWeeks.AsNoTracking()
            .Where(gw => gw.TournamentId == tournamentId)
            .OrderBy(gw => gw.WeekNumber)
            .ToListAsync();

    public async Task<GameWeek?> GetByIdAsync(Guid id) =>
        await _context.GameWeeks.AsNoTracking().FirstOrDefaultAsync(gw => gw.Id == id);

    public async Task<GameWeek> AddAsync(GameWeek gameWeek)
    {
        gameWeek.Id = Guid.NewGuid();
        _context.GameWeeks.Add(gameWeek);
        await _context.SaveChangesAsync();
        return gameWeek;
    }

    public async Task<GameWeek> UpdateAsync(GameWeek gameWeek)
    {
        _context.GameWeeks.Update(gameWeek);
        await _context.SaveChangesAsync();
        return gameWeek;
    }
}
