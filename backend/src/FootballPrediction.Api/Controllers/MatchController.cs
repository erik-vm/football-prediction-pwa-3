using System.Security.Claims;
using FootballPrediction.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers;

[ApiController]
[Route("api/v1/matches")]
[Authorize]
public class MatchController : ControllerBase
{
    private readonly IMatchService _matches;

    public MatchController(IMatchService matches) => _matches = matches;

    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcoming([FromQuery] Guid? gameWeekId)
    {
        var matches = await _matches.GetUpcomingAsync(gameWeekId);
        return Ok(matches);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var match = await _matches.GetByIdAsync(id);
            return Ok(match);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("gameweek/{gameWeekId}")]
    public async Task<IActionResult> GetByGameWeek(Guid gameWeekId)
    {
        var matches = await _matches.GetByGameWeekIdAsync(gameWeekId);
        return Ok(matches);
    }
}
