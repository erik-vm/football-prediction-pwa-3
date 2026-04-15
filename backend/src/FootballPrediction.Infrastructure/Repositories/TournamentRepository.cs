using FootballPrediction.Application.Interfaces.Repositories;
using FootballPrediction.Domain.Entities;
using FootballPrediction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballPrediction.Infrastructure.Repositories;

public class TournamentRepository : ITournamentRepository
{
    private readonly AppDbContext _context;

    public TournamentRepository(AppDbContext context) => _context = context;

    public async Task<List<Tournament>> GetAllAsync() =>
        await _context.Tournaments.AsNoTracking().OrderByDescending(t => t.StartDate).ToListAsync();

    public async Task<Tournament?> GetByIdAsync(Guid id) =>
        await _context.Tournaments.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

    public async Task<Tournament?> GetActiveAsync() =>
        await _context.Tournaments.AsNoTracking().FirstOrDefaultAsync(t => t.IsActive);

    public async Task<Tournament> AddAsync(Tournament tournament)
    {
        tournament.Id = Guid.NewGuid();
        _context.Tournaments.Add(tournament);
        await _context.SaveChangesAsync();
        return tournament;
    }

    public async Task<Tournament> UpdateAsync(Tournament tournament)
    {
        _context.Tournaments.Update(tournament);
        await _context.SaveChangesAsync();
        return tournament;
    }

    public async Task DeleteAsync(Tournament tournament)
    {
        _context.Tournaments.Remove(tournament);
        await _context.SaveChangesAsync();
    }
}
