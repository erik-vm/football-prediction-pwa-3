using FootballPrediction.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers;

[ApiController]
[Route("api/v1/tournaments")]
[Authorize]
public class TournamentController : ControllerBase
{
    private readonly ITournamentService _tournaments;
    private readonly IGameWeekService _gameWeeks;

    public TournamentController(ITournamentService tournaments, IGameWeekService gameWeeks)
    {
        _tournaments = tournaments;
        _gameWeeks = gameWeeks;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var all = await _tournaments.GetAllAsync();
        return Ok(all);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var active = await _tournaments.GetActiveAsync();
        return active == null ? NotFound() : Ok(active);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            return Ok(await _tournaments.GetByIdAsync(id));
        }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    [HttpGet("{id}/gameweeks")]
    public async Task<IActionResult> GetGameWeeks(Guid id)
    {
        var gws = await _gameWeeks.GetByTournamentIdAsync(id);
        return Ok(gws);
    }
}
