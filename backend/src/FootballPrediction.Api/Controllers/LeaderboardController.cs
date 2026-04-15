using FootballPrediction.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers;

[ApiController]
[Route("api/v1/leaderboard")]
[Authorize]
public class LeaderboardController : ControllerBase
{
    private readonly ILeaderboardService _leaderboard;

    public LeaderboardController(ILeaderboardService leaderboard) => _leaderboard = leaderboard;

    [HttpGet("overall")]
    public async Task<IActionResult> GetOverall()
    {
        var board = await _leaderboard.GetOverallAsync();
        return Ok(board);
    }

    [HttpGet("weekly/{gameWeekId}")]
    public async Task<IActionResult> GetWeekly(Guid gameWeekId)
    {
        var board = await _leaderboard.GetWeeklyAsync(gameWeekId);
        return Ok(board);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserStats(Guid userId)
    {
        try
        {
            var stats = await _leaderboard.GetUserStatsAsync(userId);
            return Ok(stats);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
