using FootballPrediction.Application.DTOs.Tournaments;
using FootballPrediction.Application.Interfaces.Repositories;
using FootballPrediction.Application.Interfaces.Services;
using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Infrastructure.Services;

public class TournamentService : ITournamentService
{
    private readonly ITournamentRepository _repo;

    public TournamentService(ITournamentRepository repo) => _repo = repo;

    public async Task<List<TournamentDto>> GetAllAsync() =>
        (await _repo.GetAllAsync()).Select(MapToDto).ToList();

    public async Task<TournamentDto> GetByIdAsync(Guid id)
    {
        var t = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Tournament not found.");
        return MapToDto(t);
    }

    public async Task<TournamentDto?> GetActiveAsync()
    {
        var t = await _repo.GetActiveAsync();
        return t == null ? null : MapToDto(t);
    }

    public async Task<TournamentDto> CreateAsync(CreateTournamentRequest request)
    {
        if (request.IsActive)
            await DeactivateAllAsync();

        var tournament = new Tournament
        {
            Name = request.Name,
            Season = request.Season,
            ExternalId = request.ExternalId,
            StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc),
            EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc),
            IsActive = request.IsActive
        };

        await _repo.AddAsync(tournament);
        return MapToDto(tournament);
    }

    public async Task<TournamentDto> UpdateAsync(Guid id, UpdateTournamentRequest request)
    {
        var tournament = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Tournament not found.");

        if (request.IsActive && !tournament.IsActive)
            await DeactivateAllAsync();

        tournament.Name = request.Name;
        tournament.Season = request.Season;
        tournament.ExternalId = request.ExternalId;
        tournament.StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
        tournament.EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);
        tournament.IsActive = request.IsActive;

        await _repo.UpdateAsync(tournament);
        return MapToDto(tournament);
    }

    public async Task DeleteAsync(Guid id)
    {
        var tournament = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Tournament not found.");
        await _repo.DeleteAsync(tournament);
    }

    private async Task DeactivateAllAsync()
    {
        var all = await _repo.GetAllAsync();
        foreach (var t in all.Where(t => t.IsActive))
        {
            t.IsActive = false;
            await _repo.UpdateAsync(t);
        }
    }

    private static TournamentDto MapToDto(Tournament t) =>
        new(t.Id, t.Name, t.Season, t.ExternalId, t.StartDate, t.EndDate, t.IsActive, t.CreatedAt);
}
