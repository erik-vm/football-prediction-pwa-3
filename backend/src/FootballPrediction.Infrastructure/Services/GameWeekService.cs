using FootballPrediction.Application.DTOs.GameWeeks;
using FootballPrediction.Application.Interfaces.Repositories;
using FootballPrediction.Application.Interfaces.Services;
using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Infrastructure.Services;

public class GameWeekService : IGameWeekService
{
    private readonly IGameWeekRepository _repo;
    private readonly ITournamentRepository _tournaments;

    public GameWeekService(IGameWeekRepository repo, ITournamentRepository tournaments)
    {
        _repo = repo;
        _tournaments = tournaments;
    }

    public async Task<List<GameWeekDto>> GetByTournamentIdAsync(Guid tournamentId) =>
        (await _repo.GetByTournamentIdAsync(tournamentId)).Select(MapToDto).ToList();

    public async Task<GameWeekDto> GetByIdAsync(Guid id)
    {
        var gw = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Game week not found.");
        return MapToDto(gw);
    }

    public async Task<GameWeekDto> CreateAsync(CreateGameWeekRequest request)
    {
        if (await _tournaments.GetByIdAsync(request.TournamentId) == null)
            throw new KeyNotFoundException("Tournament not found.");

        var gw = new GameWeek
        {
            TournamentId = request.TournamentId,
            WeekNumber = request.WeekNumber,
            Name = request.Name,
            StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc),
            EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc)
        };

        await _repo.AddAsync(gw);
        return MapToDto(gw);
    }

    public async Task<GameWeekDto> UpdateAsync(Guid id, UpdateGameWeekRequest request)
    {
        var gw = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Game week not found.");
        gw.WeekNumber = request.WeekNumber;
        gw.Name = request.Name;
        gw.StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
        gw.EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);
        await _repo.UpdateAsync(gw);
        return MapToDto(gw);
    }

    private static GameWeekDto MapToDto(GameWeek gw) =>
        new(gw.Id, gw.TournamentId, gw.WeekNumber, gw.Name, gw.StartDate, gw.EndDate);
}
