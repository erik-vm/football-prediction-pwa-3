using FootballPrediction.Application.DTOs.GameWeeks;
using FootballPrediction.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers.Admin;

[ApiController]
[Route("api/v1/admin/gameweeks")]
[Authorize(Roles = "Admin")]
public class GameWeekAdminController : ControllerBase
{
    private readonly IGameWeekService _gameWeeks;

    public GameWeekAdminController(IGameWeekService gameWeeks) => _gameWeeks = gameWeeks;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try { return Ok(await _gameWeeks.GetByIdAsync(id)); }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGameWeekRequest request)
    {
        try
        {
            var result = await _gameWeeks.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGameWeekRequest request)
    {
        try { return Ok(await _gameWeeks.UpdateAsync(id, request)); }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    [HttpPost("{id}/import-matches")]
    public async Task<IActionResult> ImportMatches(Guid id)
    {
        try
        {
            var count = await _gameWeeks.GetByIdAsync(id) != null
                ? 0
                : throw new KeyNotFoundException();

            return Ok(new { imported = count, message = "Use match service import instead." });
        }
        catch (KeyNotFoundException) { return NotFound(); }
    }
}
